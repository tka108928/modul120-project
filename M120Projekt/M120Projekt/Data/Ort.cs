using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace M120Projekt.Data
{
    public class Ort
    {
        #region Datenbankschicht
        [Key]
        public Int64 OrtId { get; set; }
        [Required]
        public String Strasse { get; set; }
        [Required]
        public Int32 Nummer { get; set; }
        [Required]
        public Int32 PLZ { get; set; }
        public String Ortname { get; set; }
        public String Land { get; set; }
        public Int32 Zeitverschiebung_in_H { get; set; }
        public ICollection<Aufgaben> Aufgaben { get; set; }
        #endregion
        #region Applikationsschicht
        public Ort() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static List<Data.Ort> LesenAlle()
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Ort.Include(x => x.Aufgaben) select record).ToList();
            }
        }
        public static Data.Ort LesenID(Int64 klasseBId)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Ort.Include(x => x.Aufgaben) where record.OrtId == klasseBId select record).FirstOrDefault();
            }
        }
        public static List<Data.Ort> LesenAttributGleich(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                var klasseBquery = (from record in context.Ort.Include(x => x.Aufgaben) where record.Strasse == suchbegriff select record).ToList();
                return klasseBquery;
            }
        }
        public static List<Data.Ort> LesenAttributWie(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Ort.Include(x => x.Aufgaben) where record.Strasse.Contains(suchbegriff) select record).ToList();
            }
        }
        public Int64 Erstellen()
        {
            if (this.Strasse == null || this.Strasse == "") this.Strasse = "leer";
            // Option mit Fehler statt Default Value
            // if (klasseB.TextAttribut == null) throw new Exception("Null ist ungültig");
            using (var context = new Data.Context())
            {
                context.Ort.Add(this);
                context.SaveChanges();
                return this.OrtId;
            }
        }
        public void Aktualisieren()
        {
            using (var context = new Data.Context())
            {
                //TODO null Checks?
                context.Entry(this).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void Loeschen()
        {
            using (var context = new Data.Context())
            {
                context.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public override string ToString()
        {
            return OrtId.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
