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
            Data.Aufgaben klasseA1 = new Data.Aufgaben();
            klasseA1.Name = "Aufgabe 1";
            klasseA1.Beschreibung = "lsdfklj jdfljsdkfljasd fsdkafk dfkjs sdfjs fksdfjlas dfdfgkdfgkjklsdfgkljdsflkjdfsg sdfgkdfkjdsfgkljdsfg kjdfgdfglkdgfkdfsglkdsfgkljdfg kldsfgkljdfglkjdfgkljdfg 5";
            klasseA1.FaelligAm = DateTime.Today;
            klasseA1.Erstelldatum = DateTime.Today;
            klasseA1.Ort = Data.Ort.LesenAttributWie("Artikelgruppe 1").FirstOrDefault();
            Int64 klasseA1Id = klasseA1.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + klasseA1Id);
        }
        // Read
        public static void DemoARead()
        {
            Debug.Print("--- DemoARead ---");
            // Demo liest alle
            foreach (Data.Aufgaben klasseA in Data.Aufgaben.LesenAlle())
            {
                Debug.Print("Artikel Id:" + klasseA.AufgabenId + " Name:" + klasseA.Name + " Artikelgruppe:" + klasseA.Ort.Strasse);
            }
        }
        // Update
        public static void DemoAUpdate()
        {
            Debug.Print("--- DemoAUpdate ---");
            // KlasseA ändert Attribute
            Data.Aufgaben klasseA1 = Data.Aufgaben.LesenID(1);
            klasseA1.Name = "Artikel 1 nach Update";
            klasseA1.OrtId = 2;  // Wichtig: Fremdschlüssel muss über Id aktualisiert werden!
            klasseA1.Aktualisieren();
        }
        // Delete
        public static void DemoADelete()
        {
            Debug.Print("--- DemoADelete ---");
            Data.Aufgaben.LesenID(1).Loeschen();
            Debug.Print("Artikel mit Id 1 gelöscht");
        }
        #endregion
        #region KlasseB
        // Create
        public static void DemoBCreate()
        {
            Debug.Print("--- DemoBCreate ---");
            // KlasseB (kurze Syntax)
            Data.Ort klasseB1 = new Data.Ort { Strasse = "Artikelgruppe 1", Zeitverschiebung_in_H = 2, Nummer = 443 };
            Int64 klasseB1Id = klasseB1.Erstellen();
            Debug.Print("Gruppe erstellt mit Id:" + klasseB1Id);
            Data.Ort klasseB2 = new Data.Ort { Strasse = "Artikelgruppe 2", Zeitverschiebung_in_H = 2, Nummer = 233 };
            Int64 klasseB2Id = klasseB2.Erstellen();
            Debug.Print("Gruppe erstellt mit Id:" + klasseB2Id);
        }
        // Read
        public static void DemoBRead()
        {
            Debug.Print("--- DemoBRead ---");
            // Demo liest 1 Objekt
            Data.Ort klasseB = Data.Ort.LesenAttributGleich("Artikelgruppe 1").FirstOrDefault();
            Debug.Print("Auslesen einzelne Gruppe mit Name: " + klasseB.Strasse + " Datum" + klasseB.Nummer.ToString("dd.MM.yyyy"));
            // Liste auslesen
            foreach(Data.Aufgaben klasseA in klasseB.Aufgaben)
            {
                Debug.Print("Artikelgruppe: " + klasseB.Strasse + " enthält Artikel:" + klasseA.Name);
            }
        }
        // Update
        public static void DemoBUpdate()
        {
            Debug.Print("--- DemoBUpdate ---");
            Data.Ort klasseB = Data.Ort.LesenID(1);
            klasseB.Strasse = "Artikelgruppe 2 nach Update";
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
                Data.Ort klasseB = Data.Ort.LesenID(1);
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
