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

        public double ScoreCourrant; 

        
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
                    if (s != null)
                    {
                        foreach (var score in s)
                        {
                            listeScore.Add(score);
                        }
                        this.Trier();
                        foreach (var score in listeScore)
                        {
                            MainWindow.main.ScoreNom.Text += score.Nom + "\n";
                            MainWindow.main.ScoreScore.Text += score.ScoreCourrant + "\n";
                            MainWindow.main.ScoreNiveau.Text += score.Niveau + "\n";
                        }
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
        public void Write()
        {
            string stockScore = JsonConvert.SerializeObject(listeScore.ToArray());
            File.WriteAllText("Score.json", stockScore);
        }
        public void Trier()
        {
            var Count = listeScore.Count;
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = i + 1; j < Count; j++)
                {
                    if (listeScore[i].ScoreCourrant < listeScore[j].ScoreCourrant)
                    {
                        var aux = listeScore[i];
                        listeScore[i] = listeScore[j];
                        listeScore[j] = aux;
                    }
                }
            }
        }
    }
}