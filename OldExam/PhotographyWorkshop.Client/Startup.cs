namespace PhotographyWorkshop.Client
{
    using Data;
    using Newtonsoft.Json;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Models;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public class Startup
    {
        public static void Main()
        {
            PhotographyWorkshopContext context = new PhotographyWorkshopContext();

            //InitializeDatabase(context);

            //ImportLenses(context);

            //ImportCameras(context);

            //ImportPhotographers(context);

            //ImportAccessories(context);

            //ImportWorkshops(context);

            //OrderedPhotographers(context);

            //LandscapePhotographers(context);

            //PhotographersWithSameCameraMake(context);

            //WorkshopsByLocation(context);
        }

        public static void InitializeDatabase(PhotographyWorkshopContext context)
        {
            context.Database.Initialize(true);
        }

        public static void ImportLenses(PhotographyWorkshopContext context)
        {
            var lensesJson = File.ReadAllText("../../Import/lenses.json");

            var lensesDTOs = JsonConvert.DeserializeObject<List<LenDTO>>(lensesJson);

            foreach (var lenseDTO in lensesDTOs)
            {
                Len len = new Len();
                len.Make = lenseDTO.Make;
                len.FocalLength = lenseDTO.FocalLength;
                len.MaxAperture = lenseDTO.MaxAperture;
                len.CompatibleWith = lenseDTO.CompatibleWith;

                context.Lenses.Add(len);
                Console.WriteLine($"Successfully imported {len.Make} {len.FocalLength}mm f{len.MaxAperture}");
            }

            context.SaveChanges();
        }

        public static void ImportCameras(PhotographyWorkshopContext context)
        {
            var camerasJson = File.ReadAllText("../../Import/cameras.json");

            var camerasDTOs = JsonConvert.DeserializeObject<List<CameraDTO>>(camerasJson);

            foreach (var cameraDTO in camerasDTOs)
            {
                string type = cameraDTO.Type;
                string make = cameraDTO.Make;
                string model = cameraDTO.Model;
                int minISO = cameraDTO.MinISO;
                bool isFoolFrame = cameraDTO.IsFullFrame;
                int? maxISO = cameraDTO.MaxISO;
                int? maxShutterSpeed = cameraDTO.MaxShutterSpeed;
                string maxVideoResolution = cameraDTO.MaxVideoResolution;
                int? maxFrameRate = cameraDTO.MaxFrameRate;

                if (type == null || make == null || model == null || minISO == 0)
                {
                    Console.WriteLine("Error. Invalid data provided");
                }
                else 
                {
                    if (type == "DSLR")
                    {
                        DSLRCamera camera = new DSLRCamera();
                        camera.Make = make;
                        camera.Model = model;
                        camera.MinISO = minISO;
                        camera.MaxISO = maxISO;
                        camera.IsFoolFrame = isFoolFrame;
                        camera.MaxShutterSpeed = maxShutterSpeed;

                        context.DSLRCameras.Add(camera);     
                    }
                    else if (type == "Mirrorless")
                    {
                        MirrorlessCamera camera = new MirrorlessCamera();
                        camera.Make = make;
                        camera.Model = model;
                        camera.MinISO = minISO;
                        camera.MaxISO = maxISO;
                        camera.IsFullFrame = isFoolFrame;
                        camera.MaxVideoResolution = maxVideoResolution;
                        camera.MaxFrameRate = maxFrameRate;

                        context.MirrorlessCameras.Add(camera);
                    }
                }

                Console.WriteLine($"Successfully imported {type} {make} {model}");
            }

            context.SaveChanges();
        }

        public static void ImportPhotographers(PhotographyWorkshopContext context)
        {
            var photogaphersJson = File.ReadAllText("../../Import/photographers.json");

            var photographersDTOs = JsonConvert.DeserializeObject<List<PhotographerDTO>>(photogaphersJson);

            foreach (var photographerDTO in photographersDTOs)
            {
                string firstName = photographerDTO.FirstName;
                string lastName = photographerDTO.LastName;
                string phone = photographerDTO.Phone;
                var lensesIds = photographerDTO.Lenses;

                if (firstName == null || lastName == null)
                {
                    Console.WriteLine("Error. Invalid data provided");
                    continue;
                }

                Photographer photographer = new Photographer();
                photographer.FirstName = firstName;

                if (lastName.Length >= 2 && lastName.Length <= 50)
                {
                    photographer.LastName = lastName;
                }

                string pattern = @"\+\d{1,3}\/\d{8,10}";
                Regex regex = new Regex(pattern);
                
                if (phone != null)
                {
                    bool hasMatch = regex.IsMatch(phone);
                    
                    if (hasMatch)
                    {
                        photographer.Phone = phone;
                    }
                }
                
                Random rnd = new Random();
                int num = 0;
                int primaryIndexDSLR = rnd.Next(1, context.DSLRCameras == null ? 1 : context.DSLRCameras.Count());
                int primaryIndexMirr = rnd.Next(1, context.MirrorlessCameras.Count());
                int secondaryIndexDSLR = rnd.Next(1, context.DSLRCameras.Count());
                int secondaryIndexMirr = rnd.Next(1, context.MirrorlessCameras.Count());

                if (num % 2 == 0)
                {
                    photographer.PrimaryCameraDSLRId = context.DSLRCameras.FirstOrDefault(c => c.Id == primaryIndexDSLR).Id;
                    photographer.SecondaryCameraMirrorlessId = context.MirrorlessCameras.FirstOrDefault(c => c.Id == secondaryIndexMirr).Id;
                }
                else
                {
                    photographer.PrimaryCameraMirrorlessId = context.MirrorlessCameras.FirstOrDefault(c => c.Id == primaryIndexMirr).Id;
                    photographer.SecondaryCameraDSLRId = context.DSLRCameras.FirstOrDefault(c => c.Id == secondaryIndexDSLR).Id;
                }

                context.Photographers.Add(photographer);
                context.SaveChanges();

                num++;

                foreach (var lenId in lensesIds)
                {
                    var currLen = context.Lenses.Find(lenId);

                    if (currLen == null)
                    {
                        continue;
                    }
                    else
                    {
                        string compatibleWith = currLen.CompatibleWith;
                        var primaryCameraDSLR = photographer.PrimaryCameraDSLR;
                        var primaryCameraMirr = photographer.PrimaryCameraMirrorless;
                        var secondaryCameraDSLR = photographer.SecondaryCameraDSLR;
                        var secondaryCameraMirr = photographer.SecondaryCameraMirrorless;

                        if (primaryCameraDSLR == null)
                        {
                            if (compatibleWith != primaryCameraMirr.Make && compatibleWith != secondaryCameraDSLR.Make)
                            {
                                continue;
                            }
                        }
                        else if (primaryCameraMirr == null)
                        {
                            if (compatibleWith != primaryCameraDSLR.Make && compatibleWith != secondaryCameraMirr.Make)
                            {
                                continue;
                            }
                        }
                    }

                    photographer.Lenses.Add(currLen);
                }

                Console.WriteLine($"Successfully imported {photographer.FirstName} {photographer.LastName} | Lenses: {photographer.Lenses.Count()}");
            }

            context.SaveChanges();
        }

        public static void ImportAccessories(PhotographyWorkshopContext context)
        {
            XDocument accessoriesXml = XDocument.Load("../../Import/accessories.xml");

            var accElements = accessoriesXml.Root.Elements();

            foreach (var accElement in accElements)
            {
                string name = accElement.Attribute("name").Value.ToString();

                Accessory accessory = new Accessory();
                accessory.Name = name;

                Random rnd = new Random();
                int index = rnd.Next(1, context.Photographers.Count());

                Photographer p = context.Photographers.Find(index);

                if (p != null)
                {
                    accessory.OwnerId = index;
                }

                context.Accessories.Add(accessory);
                Console.WriteLine($"Successfully imported {name}");
            }

            context.SaveChanges();
        }

        public static void ImportWorkshops(PhotographyWorkshopContext context)
        {
            XDocument worshopsXml = XDocument.Load("../../Import/workshops.xml");
            var workshopsElements = worshopsXml.Root.Elements();

            foreach (var workshopElement in workshopsElements)
            {
                string name = workshopElement.Attribute("name")?.Value.ToString();
                string start = workshopElement.Attribute("start-date")?.Value.ToString();
                DateTime startDate;
                bool isStartValid = DateTime.TryParse(start, out startDate);
                string end = workshopElement.Attribute("end-date")?.Value.ToString();
                DateTime endDate;
                bool isEndValid = DateTime.TryParse(end, out endDate);
                string location = workshopElement.Attribute("location")?.Value.ToString();
                string priceStr = workshopElement.Attribute("price")?.Value.ToString();
                string trainerName = workshopElement.Element("trainer")?.Value.ToString();

                if (name == null || location == null || priceStr == null || trainerName == null)
                {
                    Console.WriteLine("Error. Invalid data provided");
                    continue;
                }

                Workshop workshop = new Workshop();
                workshop.Name = name;
                workshop.Location = location;
                workshop.PricePerParticipant = decimal.Parse(priceStr);
                string[] trainerAgs = trainerName.Split(' ');
                string trainerFirstName = trainerAgs[0];
                string trainerLastName = trainerAgs[1];
                workshop.TrainerId = context.Photographers.FirstOrDefault(p => p.FirstName == trainerFirstName && p.LastName == trainerLastName).Id;

                if (isStartValid)
                {
                    workshop.StartDate = startDate;
                }

                if (isEndValid)
                {
                    workshop.EndDate = endDate;
                }

                var participants = workshopElement.Element("participants")?.Value.ToString();

                if (participants != null)
                {
                    foreach (var participant in workshopElement.Element("participants").Elements())
                    {
                        string firstName = participant.Attribute("first-name").Value;
                        string lastName = participant.Attribute("last-name").Value;

                        Photographer part = context.Photographers.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName);
                        workshop.Participants.Add(part);
                    }
                }

                context.Workshops.Add(workshop);
                Console.WriteLine($"Successfully imported {name}");
            }

            context.SaveChanges();
        }

        public static void OrderedPhotographers(PhotographyWorkshopContext context)
        {
            var photogaphers = context.Photographers
                .Select(p => new
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Phone = p.Phone
                })
                .OrderBy(p => p.FirstName)
                .ThenByDescending(p => p.LastName)
                .ToList();

            var json = JsonConvert.SerializeObject(photogaphers, Formatting.Indented);
            File.WriteAllText("../../../Export/photographers-ordered.json", json);
        }

        public static void LandscapePhotographers(PhotographyWorkshopContext context)
        {
            var photographers = context.Photographers
                .Where(p => p.PrimaryCameraDSLR != null && p.Lenses.All(l => l.FocalLength <= 30))
                .Select(p => new
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    CameraMake = p.PrimaryCameraDSLR.Make,
                    LensesCount = p.Lenses
                                   .Where(l => l.CompatibleWith == p.PrimaryCameraDSLR.Make)
                                   .Count()
                })
                .OrderBy(p => p.FirstName)
                .ToList();

            var json = JsonConvert.SerializeObject(photographers, Formatting.Indented);

            File.WriteAllText("../../../Export/landscape-photogaphers.json", json);
        }

        public static void PhotographersWithSameCameraMake(PhotographyWorkshopContext context)
        {
            var photographers = context.Photographers
                .Select(p => new
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PrimayMakeDSLR = p.PrimaryCameraDSLR.Make,
                    PrimaryModelDSLR = p.PrimaryCameraDSLR.Model,
                    PrimaryMakeMirr = p.PrimaryCameraMirrorless.Make,
                    PrimaryModelMirr = p.PrimaryCameraMirrorless.Model,
                    Lenses = p.Lenses.Where(l => (l.Make == p.PrimaryCameraDSLR.Make || l.Make == p.PrimaryCameraMirrorless.Make) && (l.Make == p.SecondaryCameraDSLR.Make || l.Make == p.SecondaryCameraMirrorless.Make))
                    .Select(l => new
                    {
                        Make = l.Make,
                        FocalLength = l.FocalLength,
                        MaxAperture = l.MaxAperture
                    })
                })
                .ToList();

            XDocument photographersXml = new XDocument();
            XElement rootElement = new XElement("photographers");

            foreach (var pho in photographers)
            {
                XElement photographerElement = new XElement("photographer");
                photographerElement.SetAttributeValue("name", pho.FirstName + " " + pho.LastName);
                string primaryCamera = pho.PrimayMakeDSLR == null ? pho.PrimaryMakeMirr : pho.PrimayMakeDSLR + " " + pho.PrimaryModelDSLR == null ? pho.PrimaryModelMirr : pho.PrimaryModelDSLR;
                photographerElement.SetAttributeValue("primary-camera", primaryCamera);

                if (pho.Lenses.Count() > 0)
                {
                    XElement lensesElement = new XElement("lenses");

                    foreach (var len in pho.Lenses)
                    {
                        XElement lensElement = new XElement("lens");
                        lensElement.Value = $"{len.Make} {len.FocalLength}mm f{len.MaxAperture}";

                        lensesElement.Add(lensElement);
                    }

                    photographerElement.Add(lensesElement);
                }

                rootElement.Add(photographerElement); 
            }

            photographersXml.Add(rootElement);
            photographersXml.Save("../../../Export/same-cameras-photographers.xml");
        }

        public static void WorkshopsByLocation(PhotographyWorkshopContext context)
        {
            var workshops = context.Workshops
                .Where(w => w.Participants.Count >= 5)
                .GroupBy(w => new { w.Location, w.Name })
                .Select(w => new
                {
                    LocationName = w.Key.Location,
                    WorkshopName = w.Key.Name,
                    TotalProfit = w.Sum(p => p.Participants.Count() * p.PricePerParticipant - 0.2M * (p.Participants.Count() * p.PricePerParticipant)),
                    ParticipantsCount = w.Sum(p => p.Participants.Count())
                })
                .ToList();

            XDocument workshopsXml = new XDocument();
            XElement rootElement = new XElement("locations");

            foreach (var workshop in workshops)
            {
                XElement locationElement = new XElement("location");
                locationElement.SetAttributeValue("name", workshop.LocationName);

                XElement workshopElement = new XElement("workshop");
                workshopElement.SetAttributeValue("name", workshop.WorkshopName);
                workshopElement.SetAttributeValue("total-profit", workshop.TotalProfit);

                XElement participantsElements = new XElement("participants");
                participantsElements.SetAttributeValue("count", workshop.ParticipantsCount);

                var currWorkshop = context.Workshops.FirstOrDefault(w => w.Name == workshop.WorkshopName);
                foreach (var participant in currWorkshop.Participants)
                {
                    XElement participantElement = new XElement("participant");
                    participantElement.Value = participant.FirstName + " " + participant.LastName;

                    participantsElements.Add(participantElement);
                }

                workshopElement.Add(participantsElements);
                locationElement.Add(workshopElement);
                rootElement.Add(locationElement);
            }

            workshopsXml.Add(rootElement);
            workshopsXml.Save("../../../Export/workshops-by-location.xml");
        }
    }
}
