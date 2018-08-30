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
    public class Aufgaben
    {
        #region Datenbankschicht
        [Key]
        public Int64 AufgabenId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Beschreibung { get; set; }
        [Required]
        public DateTime FaelligAm { get; set; }
        public Int64 Wichtigkeit { get; set; }
        public DateTime Erstelldatum { get; set; }
        public Int64 OrtId { get; set; }
        public Ort Ort { get; set; }
        #endregion
        #region Applikationsschicht
        public Aufgaben() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static List<Data.Aufgaben> LesenAlle()
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgaben.Include(x => x.Ort) select record).ToList();
            }
        }
        public static Data.Aufgaben LesenID(Int64 klasseAId)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgaben.Include(x => x.Ort) where record.AufgabenId == klasseAId select record).FirstOrDefault();
            }
        }
        public static List<Data.Aufgaben> LesenAttributGleich(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgaben.Include(x => x.Ort) where record.Name == suchbegriff select record).ToList();
            }
        }
        public static List<Data.Aufgaben> LesenAttributWie(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgaben.Include(x => x.Ort) where record.Name.Contains(suchbegriff) select record).ToList();
            }
        }
        public static List<Data.Aufgaben> LesenFremdschluesselGleich(Data.Ort suchschluessel)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgaben.Include(x => x.Ort) where record.Ort == suchschluessel select record).ToList();
            }
        }
        public Int64 Erstellen()
        {
            if (this.Name == null || this.Name == "") this.Name = "leer";
            // Option mit Fehler statt Default Value
            // if (klasseA.TextAttribut == null) throw new Exception("Null ist ungültig");
            if (this.Beschreibung == null) this.Beschreibung = "";
            using (var context = new Data.Context())
            {
                context.Aufgaben.Add(this);
                //TODO Check ob mit null möglich, sonst throw Ex
                if (this.Ort != null) context.Ort.Attach(this.Ort);
                context.SaveChanges();
                return this.AufgabenId;
            }
        }
        public Int64 Aktualisieren()
        {
            using (var context = new Data.Context())
            {
                //TODO null Checks?
                this.Ort = null;
                context.Entry(this).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return this.AufgabenId;
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
            return AufgabenId.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
