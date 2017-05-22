namespace MassDefect.Client
{
    using AutoMapper;
    using Data;
    using Models;
    using Models.DTOs;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class Startup
    {
        public static void Main()
        {
            MassDefectContext context = new MassDefectContext();

            //InitializeDatabase(context);

            //InitializeMapping();

            //ImportSolarSystems(context);

            //ImportStars(context);

            //ImportPlanets(context);

            //ImportPersons(context);

            //ImportAnomalies(context);

            //ImportAnomalyVictims(context);

            //ImportNewAnomalies(context);

            //ExportPlanetsWitchAreNotAnomalyOrigins(context);

            //ExportPeopleWitchHaveNotBeenVictims(context);

            //ExportTopAnomaly(context);

            //ExportAllAnomalies(context);
        }

        public static void InitializeDatabase(MassDefectContext context)
        {
            context.Database.Initialize(true);
        }

        public static void InitializeMapping()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SolarSystem, SolarSystemDTO>();
                cfg.CreateMap<Star, StarDTO>()
                   .ForMember(dto => dto.SolarSystem,
                              opt => opt.MapFrom(src =>
                              src.SolarSystem.Name));
                cfg.CreateMap<Planet, PlanetDTO>()
                   .ForMember(dto => dto.Sun,
                              opt => opt.MapFrom(src =>
                              src.Sun.Name))
                   .ForMember(dto => dto.SolarSystem,
                              opt => opt.MapFrom(src =>
                              src.SolarSystem.Name));
                cfg.CreateMap<Person, PersonDTO>()
                   .ForMember(dto => dto.HomePlanet,
                              opt => opt.MapFrom(src =>
                              src.HomePlanet.Name));
                cfg.CreateMap<Anomaly, AnomalyDTO>()
                   .ForMember(dto => dto.OriginPlanet,
                              opt => opt.MapFrom(src =>
                              src.OriginPlanet.Name))
                   .ForMember(dto => dto.TeleportPlanet,
                              opt => opt.MapFrom(src =>
                              src.TeleportPlanet.Name));
                cfg.CreateMap<Person, AnomalyVictimsDTO>()
                   .ForMember(dto => dto.Person,
                              opt => opt.MapFrom(src =>
                              src.Name));
                cfg.CreateMap<Anomaly, AnomalyVictimsDTO>()
                   .ForMember(dto => dto.Id,
                              opt => opt.MapFrom(src =>
                              src.Id));
            });
        }

        public static void ImportSolarSystems(MassDefectContext context)
        {
            string solarSystemsJson = File.ReadAllText("../../Import/solar-systems.json");

            List<SolarSystemDTO> solarSystemsDto = JsonConvert.DeserializeObject<List<SolarSystemDTO>>(solarSystemsJson);

            foreach (var solarSystemDto in solarSystemsDto)
            {
                SolarSystem solarSystem = new SolarSystem();
                solarSystem.Name = solarSystemDto.Name;

                context.SolarSystems.Add(solarSystem);
                Console.WriteLine($"Successfully imported Solar System {solarSystem.Name}");
            }

            context.SaveChanges();
        }

        public static void ImportStars(MassDefectContext context)
        {
            string starsJson = File.ReadAllText("../../Import/stars.json");

            List<StarDTO> starsDto = JsonConvert.DeserializeObject<List<StarDTO>>(starsJson);

            foreach (var starDto in starsDto)
            {
                Star star = new Star();
                star.Name = starDto.Name;
                SolarSystem solarSystem = context.SolarSystems.SingleOrDefault(s => s.Name == starDto.SolarSystem);

                if (solarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;   
                }

                star.SolarSystem = solarSystem;
                context.Stars.Add(star);
                Console.WriteLine($"Successfully imported Star {star.Name}");
            }

            context.SaveChanges();
        }

        public static void ImportPlanets(MassDefectContext context)
        {
            string planetsJson = File.ReadAllText("../../Import/planets.json");

            List<PlanetDTO> planetsDtos = JsonConvert.DeserializeObject<List<PlanetDTO>>(planetsJson);

            foreach (var planetDto in planetsDtos)
            {
                Planet planet = new Planet();
                planet.Name = planetDto.Name;

                Star sun = context.Stars.SingleOrDefault(s => s.Name == planetDto.Sun);
                SolarSystem solarSystem = context.SolarSystems.SingleOrDefault(s => s.Name == planetDto.SolarSystem);

                if (sun == null || solarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                planet.Sun = sun;
                planet.SolarSystem = solarSystem;

                context.Planets.Add(planet);
                Console.WriteLine($"Successfully imported Planet {planet.Name}");
            }

            context.SaveChanges();
        }

        public static void ImportPersons(MassDefectContext context)
        {
            string personsJson = File.ReadAllText("../../Import/persons.json");

            List<PersonDTO> personDtos = JsonConvert.DeserializeObject<List<PersonDTO>>(personsJson);

            foreach (var personDto in personDtos)
            {
                Person person = new Person();
                person.Name = personDto.Name;

                Planet homePlanet = context.Planets.SingleOrDefault(p => p.Name == personDto.HomePlanet);

                if (homePlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;    
                }

                person.HomePlanet = homePlanet;
                context.Persons.Add(person);
                Console.WriteLine($"Successfully imported Person {person.Name}");
            }

            context.SaveChanges();
        }

        public static void ImportAnomalies(MassDefectContext context)
        {
            string anomaliesJson = File.ReadAllText("../../Import/anomalies.json");

            List<AnomalyDTO> anomalyDtos = JsonConvert.DeserializeObject<List<AnomalyDTO>>(anomaliesJson);

            foreach (var anomalyDto in anomalyDtos)
            {
                Anomaly anomaly = new Anomaly();

                Planet originPlanet = context.Planets.SingleOrDefault(p => p.Name == anomalyDto.OriginPlanet);
                Planet teleportPlanet = context.Planets.SingleOrDefault(p => p.Name == anomalyDto.TeleportPlanet);

                if (originPlanet == null || teleportPlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue; 
                }

                anomaly.TeleportPlanet = teleportPlanet;
                anomaly.OriginPlanet = originPlanet;
                context.Anomalies.Add(anomaly);

                Console.WriteLine("Successfully imported anomaly.");
            }

            context.SaveChanges();
        }

        public static void ImportAnomalyVictims(MassDefectContext context)
        {
            string anomalyVictimsJson = File.ReadAllText("../../Import/anomaly-victims.json");

            List<AnomalyVictimsDTO> anomalyVictimDtos = JsonConvert.DeserializeObject<List<AnomalyVictimsDTO>>(anomalyVictimsJson);

            foreach (var anomalyVictimDto in anomalyVictimDtos)
            {
                Anomaly anomaly = context.Anomalies.SingleOrDefault(a => a.Id == anomalyVictimDto.Id);
                Person victim = context.Persons.SingleOrDefault(p => p.Name == anomalyVictimDto.Person);

                if (anomaly != null && victim != null)
                {
                    anomaly.Victims.Add(victim);
                    victim.Anomalies.Add(anomaly);
                }

                context.SaveChanges();
            }
        }

        public static void ImportNewAnomalies(MassDefectContext context)
        {
            XDocument anomaliesXml = XDocument.Load("../../Import/new-anomalies.xml");

            var anomaliesElements = anomaliesXml.Root.Elements();

            foreach (var ae in anomaliesElements)
            {
                string originPlanetName = ae.Attribute("origin-planet")?.Value.ToString();
                string teleportPlanetName = ae.Attribute("teleport-planet")?.Value.ToString();
                string victimName = ae.Attribute("name")?.Value.ToString();

                Anomaly anomaly = new Anomaly();
                Planet originPlanet = context.Planets.SingleOrDefault(p => p.Name == originPlanetName);
                Planet teleportPlanet = context.Planets.SingleOrDefault(p => p.Name == teleportPlanetName);
                Person victim = context.Persons.SingleOrDefault(p => p.Name == victimName);

                if (originPlanet == null || teleportPlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                anomaly.OriginPlanet = originPlanet;
                anomaly.TeleportPlanet = teleportPlanet;

                Console.WriteLine("Successfully imported anomaly.");

                if (victim != null)
                {
                    anomaly.Victims.Add(victim);
                }

                context.Anomalies.Add(anomaly);
            }

            context.SaveChanges();
        }

        public static void ExportPlanetsWitchAreNotAnomalyOrigins(MassDefectContext context)
        {
            var planets = context.Planets
                .Where(p => p.OriginAnomalies.Any(a => a.OriginPlanet.Name == p.Name) == false)
                .Select(p => new { Name = p.Name })
                .ToList();

            var planetsJson = JsonConvert.SerializeObject(planets, Formatting.Indented);
            File.WriteAllText("../../Exports/planets.json", planetsJson);
        }

        public static void ExportPeopleWitchHaveNotBeenVictims(MassDefectContext context)
        {
            var people = context.Persons
                .Where(p => p.Anomalies.Count() == 0)
                .Select(p => new
                {
                    Name = p.Name,
                    HomePlanet = new
                    {
                        Name = p.HomePlanet.Name
                    }
                })
                .ToList();

            var peopleJson = JsonConvert.SerializeObject(people, Formatting.Indented);
            File.WriteAllText("../../Exports/people.json", peopleJson);
        }

        public static void ExportTopAnomaly(MassDefectContext context)
        {
            var anomaly = context.Anomalies
                .GroupBy(a => new { a.Id, a.OriginPlanet, a.TeleportPlanet })
                .Select(a => new
                {
                    Id = a.Key.Id,
                    OriginPlanet = new
                    {
                        Name = a.Key.OriginPlanet.Name
                    },
                    TeleportPlanet = new
                    {
                        Name = a.Key.TeleportPlanet.Name
                    },
                    VictimsCount = a.Sum(c => c.Victims.Count())
                })
                .OrderByDescending(a => a.VictimsCount)
                .Take(1);

            var anomalyJson = JsonConvert.SerializeObject(anomaly, Formatting.Indented);
            File.WriteAllText("../../Exports/anomaly.json", anomalyJson);
        }

        public static void ExportAllAnomalies(MassDefectContext context)
        {
            var anomalies = context.Anomalies
                .Select(a => new
                {
                    Id = a.Id,
                    OriginPlanet = a.OriginPlanet.Name,
                    TelepportPlanet = a.TeleportPlanet.Name,
                    Victims = a.Victims.Select(v => new
                    {
                        Name = v.Name
                    })
                })
                .OrderBy(a => a.Id)
                .ToList();

            XDocument anomaliesXml = new XDocument();
            XElement rootElement = new XElement("anomalies");

            foreach (var anomaly in anomalies)
            {
                XElement anomalyElement = new XElement("anomaly");
                anomalyElement.SetAttributeValue("id", anomaly.Id);
                anomalyElement.SetAttributeValue("origin-planet", anomaly.OriginPlanet);
                anomalyElement.SetAttributeValue("teleport-planet", anomaly.TelepportPlanet);

                XElement victimsElement = new XElement("victims");

                foreach (var victim in anomaly.Victims)
                {
                    XElement victimElement = new XElement("victim");
                    victimElement.SetAttributeValue("name", victim.Name);

                    victimsElement.Add(victimElement);
                }

                anomalyElement.Add(victimsElement);
                rootElement.Add(anomalyElement);
            }

            anomaliesXml.Add(rootElement);

            anomaliesXml.Save("../../Exports/anomalies.xml");
        }
    }
}
