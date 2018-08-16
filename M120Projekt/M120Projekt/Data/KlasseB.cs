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
    public class KlasseB
    {
        #region Datenbankschicht
        [Key]
        public Int64 KlasseBId { get; set; }
        [Required]
        public String TextAttribut { get; set; }
        [Required]
        public DateTime DatumAttribut { get; set; }
        [Required]
        public Boolean BooleanAttribut { get; set; }
        public ICollection<KlasseA> FremdListeAttribut { get; set; }
        #endregion
        #region Applikationsschicht
        public KlasseB() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static List<Data.KlasseB> LesenAlle()
        {
            using (var context = new Data.Context())
            {
                return (from record in context.KlasseB.Include(x => x.FremdListeAttribut) select record).ToList();
            }
        }
        public static Data.KlasseB LesenID(Int64 klasseBId)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.KlasseB.Include(x => x.FremdListeAttribut) where record.KlasseBId == klasseBId select record).FirstOrDefault();
            }
        }
        public static List<Data.KlasseB> LesenAttributGleich(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                var klasseBquery = (from record in context.KlasseB.Include(x => x.FremdListeAttribut) where record.TextAttribut == suchbegriff select record).ToList();
                return klasseBquery;
            }
        }
        public static List<Data.KlasseB> LesenAttributWie(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.KlasseB.Include(x => x.FremdListeAttribut) where record.TextAttribut.Contains(suchbegriff) select record).ToList();
            }
        }
        public Int64 Erstellen()
        {
            if (this.TextAttribut == null || this.TextAttribut == "") this.TextAttribut = "leer";
            // Option mit Fehler statt Default Value
            // if (klasseB.TextAttribut == null) throw new Exception("Null ist ungültig");
            if (this.DatumAttribut == null) this.DatumAttribut = DateTime.MinValue;
            using (var context = new Data.Context())
            {
                context.KlasseB.Add(this);
                context.SaveChanges();
                return this.KlasseBId;
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
            return KlasseBId.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
