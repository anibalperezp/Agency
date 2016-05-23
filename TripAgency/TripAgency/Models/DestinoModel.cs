using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TripAgency.EF;

namespace TripAgency.Models
{
    public class DestinoModel
    {
        public string GetDescSpliting(string description)
        {
            string word = "";
            string[] words = description.Split(' ');
            word = words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + "...";
            return word;
        }

        public string GetDescSplitingHotel(string description)
        {
            string word = "";
            string[] words = description.Split(' ');
            for (int i = 0; i < 18; i++)
            {
                word += words[i] + " " ;
            }
            word=word + "...";
            return word;
        }

        public string GetDescSplitingCar(string description)
        {
            string word = "";
            string[] words = description.Split(' ');
            for (int i = 0; i < 10; i++)
            {
                word += words[i] + " ";
            }
            word = word + "...";
            return word;
        }

        public string GetDescSplitingDest(string description)
        {
            string word = "";
            string[] words = description.Split(' ');
            for (int i = 0; i < 6; i++)
            {
                word += words[i] + " ";
            }
            word = word + "...";
            return word;
        }
    }
}