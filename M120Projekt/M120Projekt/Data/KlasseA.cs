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
    public class KlasseA
    {
        #region Datenbankschicht
        [Key]
        public Int64 KlasseAId { get; set; }
        [Required]
        public String TextAttribut { get; set; }
        [Required]
        public DateTime DatumAttribut { get; set; }
        [Required]
        public Boolean BooleanAttribut { get; set; }
        public Int64 KlasseBId { get; set; }
        public KlasseB FremdschluesselObjekt { get; set; }
        #endregion
        #region Applikationsschicht
        public KlasseA() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static List<Data.KlasseA> LesenAlle()
        {
            using (var context = new Data.Context())
            {
                return (from record in context.KlasseA.Include(x => x.FremdschluesselObjekt) select record).ToList();
            }
        }
        public static Data.KlasseA LesenID(Int64 klasseAId)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.KlasseA.Include(x => x.FremdschluesselObjekt) where record.KlasseAId == klasseAId select record).FirstOrDefault();
            }
        }
        public static List<Data.KlasseA> LesenAttributGleich(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.KlasseA.Include(x => x.FremdschluesselObjekt) where record.TextAttribut == suchbegriff select record).ToList();
            }
        }
        public static List<Data.KlasseA> LesenAttributWie(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.KlasseA.Include(x => x.FremdschluesselObjekt) where record.TextAttribut.Contains(suchbegriff) select record).ToList();
            }
        }
        public static List<Data.KlasseA> LesenFremdschluesselGleich(Data.KlasseB suchschluessel)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.KlasseA.Include(x => x.FremdschluesselObjekt) where record.FremdschluesselObjekt == suchschluessel select record).ToList();
            }
        }
        public Int64 Erstellen()
        {
            if (this.TextAttribut == null || this.TextAttribut == "") this.TextAttribut = "leer";
            // Option mit Fehler statt Default Value
            // if (klasseA.TextAttribut == null) throw new Exception("Null ist ungültig");
            if (this.DatumAttribut == null) this.DatumAttribut = DateTime.MinValue;
            using (var context = new Data.Context())
            {
                context.KlasseA.Add(this);
                //TODO Check ob mit null möglich, sonst throw Ex
                if (this.FremdschluesselObjekt != null) context.KlasseB.Attach(this.FremdschluesselObjekt);
                context.SaveChanges();
                return this.KlasseAId;
            }
        }
        public Int64 Aktualisieren()
        {
            using (var context = new Data.Context())
            {
                //TODO null Checks?
                this.FremdschluesselObjekt = null;
                context.Entry(this).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return this.KlasseAId;
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
            return KlasseAId.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
