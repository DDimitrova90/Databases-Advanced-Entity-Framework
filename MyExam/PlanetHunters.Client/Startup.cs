namespace PlanetHunters.Client
{
    using Data;
    using Newtonsoft.Json;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Models.DTOs;
    using System.Xml.Linq;

    public class Startup
    {
        public static void Main()
        {
            PlanetHuntersContext context = new PlanetHuntersContext();
            //context.Database.Initialize(true);

            //ImportJson(context);

            //ImportXml(context);

            //ExportData(context);

            BonusTask(context);
        }

        public static void BonusTask(PlanetHuntersContext context)
        {
            Journal journal = new Journal();
            journal.Name = "New Stars";
            context.Journals.Add(journal);
            context.SaveChanges();

            var discoveries = context.Discoveries.ToList();

            foreach (var disc in discoveries)
            {
                Publication publication = new Publication();
                publication.ReleaseDate = disc.DateMade;
                publication.DiscoveryId = disc.Id;
                publication.JournalId = journal.Id;

                context.Publications.Add(publication);
            }

            context.SaveChanges();
        }

        public static void ImportJson(PlanetHuntersContext context)
        {
            //ImportAstronomers(context);

            //ImportTelescopes(context);

            //ImportPlanets(context);
        }

        public static void ImportAstronomers(PlanetHuntersContext context)
        {
            var astronomersJson = File.ReadAllText("../../Import/astronomers.json");
            var astronomers = JsonConvert.DeserializeObject<List<AstronomerDTO>>(astronomersJson);

            foreach (var astrDTO in astronomers)
            {
                string firstName = astrDTO.FirstName;
                string lastName = astrDTO.LastName;

                if (firstName == null || lastName == null)
                {
                    Console.WriteLine("Invalid data format.");
                    continue;
                }

                if (firstName.Length > 50 || lastName.Length > 50)
                {
                    Console.WriteLine("Invalid data format.");
                    continue;
                }

                Astronomer astronomer = new Astronomer();
                astronomer.FirstName = firstName;
                astronomer.LastName = lastName;

                context.Astronomers.Add(astronomer);
                Console.WriteLine($"Record {firstName} {lastName} successfully imported.");
            }

            context.SaveChanges();
        }

        public static void ImportTelescopes(PlanetHuntersContext context)
        {
            var telescopesJson = File.ReadAllText("../../Import/telescopes.json");
            var telescopesDTOs = JsonConvert.DeserializeObject<List<TelescopeDTO>>(telescopesJson);

            foreach (var telescopeDTO in telescopesDTOs)
            {
                string name = telescopeDTO.Name;
                string location = telescopeDTO.Location;
                string mirrorDiameterStr = telescopeDTO.MirrorDiameter;

                if (name == null || location == null)
                {
                    Console.WriteLine("Invalid data format.");
                    continue;
                }

                if (name.Length > 255 || location.Length > 255)
                {
                    Console.WriteLine("Invalid data format.");
                    continue;
                }

                Telescope telescope = new Telescope();
                telescope.Name = name;
                telescope.Location = location;

                if (mirrorDiameterStr != null)
                {
                    double mirrorDiameter = double.Parse(mirrorDiameterStr);

                    if (mirrorDiameter <= 0)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }

                    telescope.MirrorDiameter = mirrorDiameter;
                }

                context.Telescopes.Add(telescope);
                Console.WriteLine($"Record {name} successfully imported.");
            }

            context.SaveChanges();
        }

        public static void ImportPlanets(PlanetHuntersContext context)
        {
            var planetsJson = File.ReadAllText("../../Import/planets.json");
            var planetsDTOs = JsonConvert.DeserializeObject<List<PlanetDTO>>(planetsJson);

            foreach (var planetDTO in planetsDTOs)
            {
                string name = planetDTO.Name;
                string massStr = planetDTO.Mass;
                string starSystem = planetDTO.StarSystem;

                if (name == null || massStr == null || starSystem == null)
                {
                    Console.WriteLine("Invalid data format.");
                    continue;
                }

                if (name.Length > 255)
                {
                    Console.WriteLine("Invalid data format.");
                    continue;
                }

                StarSystem currStarSystem = context.StarSystems.SingleOrDefault(s => s.Name == starSystem);

                Planet planet = new Planet();
                planet.Name = name;
                planet.Mass = double.Parse(massStr);

                if (currStarSystem == null)
                {
                    StarSystem starSys = new StarSystem();
                    starSys.Name = starSystem;
                    context.StarSystems.Add(starSys);
                    context.SaveChanges();
                }

                planet.HostStarSystemId = context.StarSystems.SingleOrDefault(s => s.Name == starSystem).Id;

                context.Planets.Add(planet);
                Console.WriteLine($"Record {name} successfully imported.");
            }

            context.SaveChanges();
        }

        public static void ImportXml(PlanetHuntersContext context)
        {
            //ImportStars(context);

            //ImportDiscoveries(context);
        }

        public static void ImportStars(PlanetHuntersContext context)
        {
            XDocument startsXml = XDocument.Load("../../Import/stars.xml");
            var starsElements = startsXml.Root.Elements();

            foreach (var starElement in starsElements)
            {
                string name = starElement.Element("Name")?.Value.ToString();
                string tempStr = starElement.Element("Temperature")?.Value.ToString();
                string starSystemName = starElement.Element("StarSystem")?.Value.ToString();

                if (name == null || tempStr == null || starSystemName == null)
                {
                    Console.WriteLine("Invalid data format.");
                    continue;
                }

                int temperature = int.Parse(tempStr);

                if (name.Length > 255 || temperature < 2400)
                {
                    Console.WriteLine("Invalid data format.");
                    continue;
                }

                Star star = new Star();
                star.Name = name;
                star.Temperature = temperature;

                StarSystem currStarSystem = context.StarSystems.SingleOrDefault(s => s.Name == starSystemName);

                if (currStarSystem == null)
                {
                    StarSystem starSystem = new StarSystem();
                    starSystem.Name = starSystemName;
                    context.StarSystems.Add(starSystem);
                    context.SaveChanges();
                }

                star.HostStarSystemId = context.StarSystems.SingleOrDefault(s => s.Name == starSystemName).Id;

                context.Stars.Add(star);
                Console.WriteLine($"Record {name} successfully imported.");
            }

            context.SaveChanges();
        }

        public static void ImportDiscoveries(PlanetHuntersContext context)
        {
            XDocument discoveriesXml = XDocument.Load("../../Import/discoveries.xml");
            var discoveriesElements = discoveriesXml.Root.Elements();

            foreach (var discElement in discoveriesElements)
            {
                string dateStr = discElement.Attribute("DateMade").Value.ToString();
                string telescopeName = discElement.Attribute("Telescope").Value.ToString();

                if (dateStr == null || telescopeName == null)
                {
                    continue;
                }

                Discovery discovery = new Discovery();
                DateTime dateMade;
                bool isDateValid = DateTime.TryParse(dateStr, out dateMade);
                discovery.DateMade = dateMade;
                Telescope currTelescope = context.Telescopes.FirstOrDefault(t => t.Name == telescopeName);

                if (currTelescope == null)
                {
                    continue;
                }

                discovery.TelescopeId = context.Telescopes
                    .FirstOrDefault(t => t.Name == telescopeName)
                    .Id;

                var starsElements = discElement.Element("Stars").Elements();
                bool isStarExists = true;

                foreach (var starElement in starsElements)
                {
                    string starName = starElement.Value.ToString();

                    Star currStar = context.Stars.FirstOrDefault(s => s.Name == starName);

                    if (currStar == null)
                    {
                        isStarExists = false;
                        break;
                    }

                    discovery.Stars.Add(currStar);
                }

                if (!isStarExists)
                {
                    continue;
                }

                var planetsElements = discElement.Element("Planets").Elements();
                bool isPlanetExists = true;

                foreach (var planetElement in planetsElements)
                {
                    string planetName = planetElement.Value.ToString();

                    Planet currPlanet = context.Planets.FirstOrDefault(p => p.Name == planetName);

                    if (currPlanet == null)
                    {
                        isPlanetExists = false;
                        break;
                    }

                    discovery.Planets.Add(currPlanet);
                }

                if (!isPlanetExists)
                {
                    continue;
                }

                var pioneersElements = discElement.Element("Pioneers").Elements();
                bool isPioneerExists = true;

                foreach (var pioneerElement in pioneersElements)
                {
                    string pioneerFullName = pioneerElement.Value.ToString();
                    string[] nameArgs = pioneerFullName.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string pioneerFirstName = nameArgs[1];
                    string pioneerLastName = nameArgs[0];

                    Astronomer currPioneer = context.Astronomers.FirstOrDefault(a => a.FirstName == pioneerFirstName && a.LastName == pioneerLastName);

                    if (currPioneer == null)
                    {
                        isPioneerExists = false;
                        break;
                    }

                    discovery.Pioneers.Add(currPioneer);
                }

                if (!isPioneerExists)
                {
                    continue;
                }

                var observersElements = discElement.Element("Observers").Elements();
                bool isObserverExists = true;

                foreach (var observerElement in observersElements)
                {
                    string observerFullName = observerElement.Value.ToString();
                    string[] nameArgs = observerFullName.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string observerFirstName = nameArgs[1];
                    string observerLastName = nameArgs[0];

                    Astronomer currObserver = context.Astronomers.FirstOrDefault(a => a.FirstName == observerFirstName && a.LastName == observerLastName);

                    if (currObserver == null)
                    {
                        isObserverExists = false;
                        break;
                    }

                    discovery.Observers.Add(currObserver);
                }

                if (!isObserverExists)
                {
                    continue;
                }

                context.Discoveries.Add(discovery);
                Console.WriteLine($"Discovery ({dateMade.Year}/{dateMade.Month}/{dateMade.Day}-{telescopeName}) with {discovery.Stars.Count} star(s), {discovery.Planets.Count()} planet(s), {discovery.Pioneers.Count()} pioneer(s) and {discovery.Observers.Count()} observers successfully  imported.");
            }

            context.SaveChanges();
        }

        public static void ExportData(PlanetHuntersContext context)
        {
            //ExportPlanets(context);

            //ExportAstronomers(context);

            //ExportStars(context);
        }

        public static void ExportPlanets(PlanetHuntersContext context)
        {
            string telescopeName = Console.ReadLine();

            var planets = context.Planets
                .Where(p => p.Discovery.Telescope.Name == telescopeName)
                .Select(p => new
                {
                    Name = p.Name,
                    Mass = p.Mass,
                    Orbiting = context.Stars
                                      .Where(s => s.HostStarSystem.Name == p.HostStarSystem.Name)
                                      .Select(s => new
                                      {
                                          s.Name
                                      })

                })
                .OrderByDescending(p => p.Mass)
                .ToList();

            var planetsJson = JsonConvert.SerializeObject(planets, Formatting.Indented);
            File.WriteAllText($"../../../Export/planets-by-{telescopeName}.json", planetsJson);
        }

        public static void ExportAstronomers(PlanetHuntersContext context)
        {
            string starSystemName = Console.ReadLine();

            var discoveries = context.Discoveries
                .Where(d => 
                d.Planets.All(p => p.HostStarSystem.Name == starSystemName) && 
                d.Stars.All(s => s.HostStarSystem.Name == starSystemName));   
        }

        public static void ExportStars(PlanetHuntersContext context)
        {
            var stars = context.Stars
                .Select(s => new
                {
                    Name = s.Name,
                    Temperature = s.Temperature,
                    StarSystemName = s.HostStarSystem.Name,
                    DiscoveryDate = s.Discovery.DateMade,
                    TelescopeName = s.Discovery.Telescope.Name,
                    Astronomers = s.Discovery.Pioneers.Select(p => new
                    {
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Role = "pioneer"
                    }).Concat(s.Discovery.Observers.Select(o => new
                    {
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        Role = "observer"
                    })).OrderBy(a => a.FirstName)
                })
                .ToList();

            XDocument starsXml = new XDocument();
            XElement rootElement = new XElement("Stars");

            foreach (var star in stars)
            {
                XElement starElement = new XElement("Star");

                XElement nameElement = new XElement("Name");
                nameElement.Value = star.Name;
                XElement tempElement = new XElement("Temperature");
                tempElement.Value = star.Temperature.ToString();
                XElement starSystemElement = new XElement("StarSystem");
                starSystemElement.Value = star.StarSystemName;
                XElement discoveryInfoElement = new XElement("DiscoveryInfo");
                string date = star.DiscoveryDate.Year + "-" + star.DiscoveryDate.Month + "-" + star.DiscoveryDate.Day;
                discoveryInfoElement.SetAttributeValue("DiscoveryDate", date);
                discoveryInfoElement.SetAttributeValue("TelescopeName", star.TelescopeName);

                foreach (var astr in star.Astronomers)
                {
                    XElement astronomerElement = new XElement("Astronomer");

                    string role;

                    if (astr.Role == "pioneer")
                    {
                        role = "true";
                    }
                    else
                    {
                        role = "false";
                    }

                    astronomerElement.SetAttributeValue("Pioneer", role);
                    astronomerElement.Value = astr.FirstName + " " + astr.LastName;

                    discoveryInfoElement.Add(astronomerElement);
                }

                starElement.Add(nameElement);
                starElement.Add(tempElement);
                starElement.Add(starSystemElement);
                starElement.Add(discoveryInfoElement);
                rootElement.Add(starElement);
            }

            starsXml.Add(rootElement);

            starsXml.Save("../../../Export/stars.xml");
        }
    }
}
