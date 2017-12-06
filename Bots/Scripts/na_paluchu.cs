using Bots.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
using System.Net;

namespace Bots.Scripts
{
    class na_paluchu : Iscrappe
    {
        public List<ListToAdd> NewList = new List<ListToAdd>();
        public void start()
        {
            var shelter_id = Shelter_ID.na_paluchu;
            var data = scrap(shelter_id);
            if (update.na_paluchu)
            {
                Console.WriteLine(Shelter_ID.na_paluchu.Name + "Start");

                if (config.inDevelopment)
                {
                    
                }
                if (config.updateDB)
                {
                    // var data = scrap(shelter_id);
                }
            }
        }
        
    

        public List<ListTemp> scrap(Shelter_ID.id shelter_id)
        {
            
            HtmlWeb web_page = new HtmlWeb();
            List<ListTemp> listTemp = new List<ListTemp>();
            DateTime today = DateTime.Today;
            string _name = "";
            string _description = "";
            string _breed = "";
            string _gender = "";
            string _age = "";
            string _weight = "";
            DateTime _dateStart = DateTime.Today;

  


            string url = @"http://www.napaluchu.waw.pl/czekam_na_ciebie/wszystkie_zwierzeta_do_adopcji:1";

            var doc = web_page.Load(url);
            int number = 1;
            bool allList = false;
            while (!allList)
            {
                
                var nextPage = doc.DocumentNode.SelectNodes("//div[@class = 'pagination']/a[@class = 'next']");
                number++;
                url = "http://www.napaluchu.waw.pl/czekam_na_ciebie/wszystkie_zwierzeta_do_adopcji:" + number;
                
                var animal_link = doc.DocumentNode.SelectNodes("//a[@class = 'animals_btn_list_more']/@href").Select(q => q.GetAttributeValue("href", null)).ToList();

                for (int i = 0; i < animal_link.Count(); i++)
                {
                    List<byte[]> _photo = new List<byte[]>();
                    var animal_doc = web_page.Load(@"http://www.napaluchu.waw.pl" + animal_link[i]);

                    //--INFO
                    var nodeInfo = animal_doc.DocumentNode.SelectNodes("//div[@class = 'info']")[0].InnerText.Replace("\r", "").Replace("\n", "").Trim();
                    nodeInfo = HtmlEntity.DeEntitize(nodeInfo).Trim();
                    var tempInfo = nodeInfo.Split(':');

                    for (int t = 0; t < tempInfo.Count(); t++)
                    {
                        if(tempInfo[t].Contains("Gatunek"))
                        {
                            _name = tempInfo[t].Replace("Gatunek", "").Trim();
                        }      
                        if (tempInfo[t].Contains("Płeć"))
                        {
                            _breed = tempInfo[t].Replace("Płeć", "").Trim();
                        }
                        if (tempInfo[t].Contains("Wiek"))
                        {
                            _gender = tempInfo[t].Replace("Wiek", "").Trim();
                        }
                        if (tempInfo[t].Contains("Waga"))
                        {
                            _age = tempInfo[t].Replace("Waga", "").Replace("lat", "").Replace("rok", "").Trim();
                        }
                        if (tempInfo[t].Contains("Data przyjęcia"))
                        {
                            _weight = tempInfo[t].Replace("Data przyjęcia", "").Trim();
                        }
                        if (tempInfo[t].Contains("ewidencyjny"))
                        {
                            var year = int.Parse(tempInfo[t].Replace("Nr ewidencyjny", "").Trim().Split('.')[2]);
                            var month = int.Parse(tempInfo[t].Replace("Nr ewidencyjny", "").Trim().Split('.')[1]);
                            var day = int.Parse(tempInfo[t].Replace("Nr ewidencyjny", "").Trim().Split('.')[0]);
                            _dateStart = new DateTime(year, month, day);
                        }
                    }//--INFO

                    //--Description
                    _description = "";
                    
                    var nodeDescription = animal_doc.DocumentNode.SelectNodes("//div[@class = 'description']").Select(q => q.InnerText).ToList();
     
                    for (int d = 0; d < nodeDescription.Count(); d++)
                    {
                        _description += nodeDescription[d];
                    }
                    _description = HtmlEntity.DeEntitize(_description).Replace("\r", " ").Replace("\n", "").Trim();
                    //--Description

                    //--Photo 
                    var node_Photo = animal_doc.DocumentNode.SelectNodes("//div[@class = 'ani_images']/div[@class = 'ani_image_bottom']/a");
                    if(node_Photo != null)
                    {
                        var nodePhoto = node_Photo.Select(q => q.GetAttributeValue("href", null)).ToList();
                        var photoLink = @"http://www.napaluchu.waw.pl";
                        
                        for(int p = 0; p < nodePhoto.Count(); p++)
                        {
                            using(var client = new WebClient())
                            {
                                _photo.Add(client.DownloadData(photoLink + nodePhoto[p]));
                            }
                            if(p == 4)
                            {
                                break;    
                            }
                        }
                        
                    }
              
                    //--Photo
                   
                listTemp.Add(new ListTemp()
                {
                    name = _name,
                    breed = _breed,
                    gender = _gender,
                    age = _age,
                    weight = _weight,
                    description = _description,
                    dateStart = _dateStart,
                    shelter_ID = shelter_id.ID,
                    photo = _photo,
                });
                    
                }   

                doc = web_page.Load(url);
                if(nextPage == null)
                {
                    allList = true;
                }
            }


            return listTemp;
        }
    }
}
