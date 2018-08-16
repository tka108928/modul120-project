using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M120Projekt
{
    static class APIDemo
    {
        #region KlasseA
        // Create
        public static void DemoACreate()
        {
            Debug.Print("--- DemoACreate ---");
            // KlasseA (lange Syntax)
            Data.KlasseA klasseA1 = new Data.KlasseA();
            klasseA1.TextAttribut = "Artikel 1";
            klasseA1.DatumAttribut = DateTime.Today;
            klasseA1.FremdschluesselObjekt = Data.KlasseB.LesenAttributWie("Artikelgruppe 1").FirstOrDefault();
            Int64 klasseA1Id = klasseA1.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + klasseA1Id);
        }
        // Read
        public static void DemoARead()
        {
            Debug.Print("--- DemoARead ---");
            // Demo liest alle
            foreach (Data.KlasseA klasseA in Data.KlasseA.LesenAlle())
            {
                Debug.Print("Artikel Id:" + klasseA.KlasseAId + " Name:" + klasseA.TextAttribut + " Artikelgruppe:" + klasseA.FremdschluesselObjekt.TextAttribut);
            }
        }
        // Update
        public static void DemoAUpdate()
        {
            Debug.Print("--- DemoAUpdate ---");
            // KlasseA ändert Attribute
            Data.KlasseA klasseA1 = Data.KlasseA.LesenID(1);
            klasseA1.TextAttribut = "Artikel 1 nach Update";
            klasseA1.KlasseBId = 2;  // Wichtig: Fremdschlüssel muss über Id aktualisiert werden!
            klasseA1.Aktualisieren();
        }
        // Delete
        public static void DemoADelete()
        {
            Debug.Print("--- DemoADelete ---");
            Data.KlasseA.LesenID(1).Loeschen();
            Debug.Print("Artikel mit Id 1 gelöscht");
        }
        #endregion
        #region KlasseB
        // Create
        public static void DemoBCreate()
        {
            Debug.Print("--- DemoBCreate ---");
            // KlasseB (kurze Syntax)
            Data.KlasseB klasseB1 = new Data.KlasseB { TextAttribut = "Artikelgruppe 1", BooleanAttribut = true, DatumAttribut = DateTime.Today.AddDays(-1) };
            Int64 klasseB1Id = klasseB1.Erstellen();
            Debug.Print("Gruppe erstellt mit Id:" + klasseB1Id);
            Data.KlasseB klasseB2 = new Data.KlasseB { TextAttribut = "Artikelgruppe 2", BooleanAttribut = true, DatumAttribut = DateTime.Today };
            Int64 klasseB2Id = klasseB2.Erstellen();
            Debug.Print("Gruppe erstellt mit Id:" + klasseB2Id);
        }
        // Read
        public static void DemoBRead()
        {
            Debug.Print("--- DemoBRead ---");
            // Demo liest 1 Objekt
            Data.KlasseB klasseB = Data.KlasseB.LesenAttributGleich("Artikelgruppe 1").FirstOrDefault();
            Debug.Print("Auslesen einzelne Gruppe mit Name: " + klasseB.TextAttribut + " Datum" + klasseB.DatumAttribut.ToString("dd.MM.yyyy"));
            // Liste auslesen
            foreach(Data.KlasseA klasseA in klasseB.FremdListeAttribut)
            {
                Debug.Print("Artikelgruppe: " + klasseB.TextAttribut + " enthält Artikel:" + klasseA.TextAttribut);
            }
        }
        // Update
        public static void DemoBUpdate()
        {
            Debug.Print("--- DemoBUpdate ---");
            Data.KlasseB klasseB = Data.KlasseB.LesenID(1);
            klasseB.TextAttribut = "Artikelgruppe 2 nach Update";
            klasseB.Aktualisieren();
            Debug.Print("Gruppe mit Name 'Artikelgruppe 1' verändert");
        }
        // Delete
        public static void DemoBDelete()
        {
            Debug.Print("--- DemoBDelete ---");
            // Achtung! Referentielle Integrität darf nicht verletzt werden!
            try
            {
                Data.KlasseB klasseB = Data.KlasseB.LesenID(1);
                klasseB.Loeschen();
                Debug.Print("Gruppe mit Id 1 gelöscht");
            } catch (Exception ex)
            {
                Debug.Print("Fehler beim Löschen:" + ex.Message);
            }
        }
        #endregion
    }
}
