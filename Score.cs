using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tetris
{
    class Score
    {
        public static List<Score> listeScore = new List<Score>();
        public int Niveau { get; set; }
        public double ScoreCourrant { get; set; }
        public string Nom { get; set; }

        public Score()
        {

        }
        public void Read()
        {
            if (File.Exists("Score.json"))
            {
                using (StreamReader r = new StreamReader("Score.json"))
                {
                    string json = r.ReadToEnd();
                    var s = JsonConvert.DeserializeObject<List<Score>>(json);
                    foreach (var score in s)
                    {
                        listeScore.Add(score);
                    }
                }
            }
            else
            {
                File.Create("Score.json");
            }
        }
        public void Enregistrer(int Niveau, double ScoreCourrant, string Nom)
        {
            this.Niveau = Niveau;
            this.ScoreCourrant = ScoreCourrant;
            this.Nom = Nom;
            listeScore.Add(this);
        }
        private void Write()
        {
            string stockScore = JsonConvert.SerializeObject(listeScore.ToArray());
            File.WriteAllText("Score.json", stockScore);
        }
    }
}