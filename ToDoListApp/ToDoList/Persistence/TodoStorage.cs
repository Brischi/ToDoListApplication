using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; //bietet Klassen und Methoden für den Zugriff auf das Dateisystem. Kann damit Dateien erstellen, lesen, schreiben, löschen und überprüfen und mit Verzeichnissen und Streams arbeiten
using System.Threading.Tasks;
using System.Text.Json;
using ToDoList.View;
using ToDoList.Model;

namespace ToDoList.Persistence
{
    public static class TodoStorage //statisch (nicht instanzierbar), weil diese Klasse keine eigenen Objekte erstellt und keinen eigenen Zustand hat, sondern ihre Methoden direkt aufgerufen werden. Sie bietet nur allg. Funktionen (save/load) für andere Klassen.Instanziierbare Klassen: Jede Instanz (jedes Objekt) der Klasse hat ihren eigenen Zustand (Eigenschaften) und funktioniert unabhängig von anderen Instanzen. 

    {
        private static readonly string FilePath;
        //FilePath definiert Namen der JSON-Datei. Const um Konstante zu definieren (Werte, die während der Laufzeit nicht geändert werden können). Heißt, der Wert wird einmalig zur Kompilierungszeit festgelegt und bleibt danach unveränderlich. bei readonly ist das anders

        static TodoStorage()  // Statischer Konstruktor für die Initialisierung (wird einmal aufgerufen, bevor die Klasse das erste Mal verwendet wird
        {
            // Erstellt einen Unterordner "ToDoList" in AppData
            string directory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ToDoList" //Path ist eine Klasse aus System.IO, die Werkzeuge für das Arbeiten mit Dateipfaden bereitstellt.
            );

            //Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) liefert den Pfad zum AppData-Verzeichnis der aktuellen Benutzerin
            //Path.Combine kombiniert den AppData-Pfad mit "Dashboard", um neuen Unterordner zu definieren

            // Verzeichnis erstellen, falls es nicht existiert
            if (!Directory.Exists(directory)) //Directory ist eine Klasse aus System.IO, die Funktionen für das Arbeiten mit Verzeichnissen bereitstellt
            {
                Directory.CreateDirectory(directory);
            }

            // Dateipfad initalisieren
            FilePath = Path.Combine(directory, "TodoItems.json");
        }

        // Speichert die Liste der To-Do-Items in einer JSON-Datei
        public static void Save(List<TodoItem> todoItems)
        {
            string json = JsonSerializer.Serialize(todoItems, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(FilePath, json); // File ist eine Klasse aus System.IO, die Methoden für den direkten Zugriff auf Dateien bereitstellt
        } 

        // Lädt die Liste der To-Do-Items aus einer JSON-Datei

        public static List<TodoItem> Load()
        {
            // Überprüft, ob die Datei existiert
            if (!File.Exists(FilePath))
                return new List<TodoItem>(); // Gibt eine leere Liste zurück, wenn die Datei nicht existiert

            try //bei fehleranfälligem code. wenn es hier zu einer Exeption kommt, wird in den catch-Block geleitet. (zB Beschädigte Datei, Probleme beim Lesen der Datei/keine Berechtigung, ...)
            {

                string json = File.ReadAllText(FilePath); // Liest den Inhalt der JSON-Datei

                // Wandelt den JSON-String in eine Liste von To-Do-Items um
                // Falls die Datei leer ist oder null zurückgibt, wird eine neue leere Liste erstellt
                return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
            }
            catch //hier dann code zur Fehlerbehandlung. Gibt eine leere Liste zurück, wenn ein Fehler auftritt
            {
                return new List<TodoItem>();
            }
        }
    }
}
