using System.Linq;
namespace Universitetssystem_oblg;

class Program
{  
    static void Main()
    {
        List<Kurs> kursliste = new List<Kurs>();
        List<Student> studenter = new List<Student>();
        List<Bøker> bøker = new List<Bøker>();
        List<Lån> låneHistorikk = new List<Lån>();
        List<Ansatt> ansatte = new List<Ansatt>();
        
        bool startup = true;
        while (startup)
        {
            Console.WriteLine("1. Opprett kurs");
            Console.WriteLine("2. Meld student til kurs");
            Console.WriteLine("3. Print kurs og deltagere");
            Console.WriteLine("4. Søk på kurs");
            Console.WriteLine("5. Søk på bok");
            Console.WriteLine("6. Lån bok");
            Console.WriteLine("7. Returner bok");
            Console.WriteLine("8. Registrer bok");
            Console.WriteLine("0. Avslutt");
            Console.Write("Valg: ");
            
            string valg = Console.ReadLine();
            switch (valg)
            {
                case "1": OpprettKurs(kursliste); break;
                case "2": MeldPåKurs(kursliste, studenter); break;
                case "3": PrintKurs(kursliste); break;
                case "4": SøkPåKurs(kursliste); break;
                case "5": SøkPåBok(bøker); break;
                case "6": Lånebok(bøker,studenter, ansatte, låneHistorikk); break;
                case "7": ReturnBok(bøker, studenter, ansatte, låneHistorikk); break;
                case "8": RegistrerBok(bøker); break;
                case "0": Avslutt(); break;
            }

            string[] GyldigeValg = { "1", "2", "3", "4", "5", "6", "7", "8", "0" };
            if (!GyldigeValg.Contains(valg))
            {
                Console.WriteLine("Skriv inn et gyldig svar: ");
            }
        }
            
    }
    //Metodene
    static void OpprettKurs(List<Kurs> kursliste)
    {
        Kurs nyttKurs = new Kurs();
        Console.Write("Kurskode: ");
        nyttKurs.KursKode = Console.ReadLine();
        Console.Write("Kursnavn: ");
        nyttKurs.KursNavn = Console.ReadLine();
        Console.Write("Studiepoeng: ");
        nyttKurs.StudiePoeng = int.Parse(Console.ReadLine());
        Console.Write("Maks kurssplasser: ");
        nyttKurs.MaksPlasser = int.Parse(Console.ReadLine());
        
        kursliste.Add(nyttKurs);
        Console.Write($"Kurset ´ {nyttKurs.KursNavn}´ er oprettet!!");
    }

    static void MeldPåKurs(List<Kurs> kursliste, List<Student> studenter)
    {
        Console.Write("Skriv inn kurskode: ");
        string kurskode = Console.ReadLine().ToLower();
        
        Kurs kurs = kursliste.FirstOrDefault(k => k.KursKode.ToLower() == kurskode);
        if (kurs == null)
        {
            Console.WriteLine("Fant ikke kurset");
            return;
        }
        
        Console.WriteLine("1. Meld på kurs");
        Console.WriteLine("2. Meld av kurs");
        Console.Write("Valg: ");
        string valg = Console.ReadLine();

        if (valg == "1")
        {
            if (kurs.Deltakere.Count >= kurs.MaksPlasser)
            {
                Console.WriteLine("Kurset er fullt");
                return;
            }

            Console.Write("Skriv inn StudentID: ");
            int studentID = int.Parse(Console.ReadLine());

            Student student = studenter.FirstOrDefault(s => s.StudentID == studentID);
            if (student == null)
            {
                Console.WriteLine("Fant ikke studenten");
                return;
            }

            if (kurs.Deltakere.Contains(student))
            {
                Console.WriteLine("Studenten er allerede meldt på dette kurset. ");
                return;
            }

            kurs.Deltakere.Add(student);
            student.Kurs.Add(kurs);
            Console.WriteLine($"{student.Navn} er meldt på {kurs.KursNavn}!");
        }
        else if (valg == "2")
        {
            Console.Write("Skriv inn StudentID: ");
            int studentID = int.Parse(Console.ReadLine());
            Student student = studenter.FirstOrDefault(s => s.StudentID == studentID);
            if (student == null)
            {
                Console.WriteLine("Fant ikke studenten");
                return;
            }

            if (kurs.Deltakere.Contains(student))
            {
                Console.WriteLine("Studenten er ikke meldt på dette kurset. ");
                return;
            }

            kurs.Deltakere.Remove(student);
            student.Kurs.Remove(kurs);
            Console.WriteLine($"{student.Navn} er nå meldt av {kurs.KursNavn}.");
        }
        else
        {
            Console.WriteLine("Ugyldig valg.");
        }
    }

    static void PrintKurs(List<Kurs> kursliste)
    {
        if (kursliste.Count == 0)
        {
            Console.WriteLine("Ingen kurs er oprettet ennå.");
            return;
        }

        foreach (Kurs kurs in kursliste)
        {
            Console.WriteLine($"[{kurs.KursKode}] {kurs.KursNavn} - {kurs.StudiePoeng} stp - {kurs.MaksPlasser} plasser" );

            if (kurs.Deltakere.Count == 0)
            {
                Console.WriteLine("Ingen deltakere påmeldt");
            }
            else
            {
                foreach (Student student in kurs.Deltakere)
                {
                    Console.WriteLine($"- [{student.StudentID}] {student.Navn}");
                }
            }
        }
    }

    static void SøkPåKurs(List<Kurs> kursliste)
    {
        Console.WriteLine("Vennligst skriv inn kurskode eller kursnavn: ");
        string søkemotor = Console.ReadLine().ToLower();

        List<Kurs> resultater = kursliste
            .Where(k => k.KursKode.ToLower().Contains(søkemotor) || k.KursNavn.ToLower().Contains(søkemotor)).ToList();

        if (resultater.Count == 0)
        {
            Console.WriteLine("Ingen kurs funnet.");
            return;
        }

        foreach (Kurs kurs in resultater)
        {
            Console.WriteLine($"[{kurs.KursKode}]{kurs.KursNavn} - {kurs.StudiePoeng} stp");
        }
    }

    static void SøkPåBok(List<Bøker> bøker)
    {
        Console.Write("Skriv in tittel eller forfatter");
        string søk = Console.ReadLine().ToLower();

        List<Bøker> resultater = bøker
            .Where(b => b.Tittel.ToLower().Contains(søk) || b.Forfatter.ToLower().Contains(søk)).ToList();

        if (resultater.Count == 0)
        {
            Console.WriteLine("Ingen tittel eller forfatter.");
            return;
        }

        foreach (Bøker bok in resultater)
        {
            Console.WriteLine(
                $"[{bok.BokID}]{bok.Tittel} - {bok.Forfatter} ({bok.UtgivelseÅr}) - {bok.TilgjengeligeEksemplarer} tilgjengelig");
        }
    }

    static void Lånebok(List<Bøker> bøker, List<Student> studenter, List<Ansatt> ansatte, List<Lån> låneHistorikk)
    {
        Console.Write("Skriv inn BokID");
        int bokID = int.Parse(Console.ReadLine());
        
        Bøker bok = bøker.FirstOrDefault(b => b.BokID == bokID); //for å se
        if (bok == null)
        {
            Console.WriteLine("Fant ikke boken");
            return;
        }

        if (bok.TilgjengeligeEksemplarer == 0)
        {
            Console.WriteLine("Ingen tilgjengelige eksemplarer");
            return;
        }
        
        Console.WriteLine("Er du student eller ansatt? s/a: ");
        string type = Console.ReadLine().ToLower(); //Lower i tilefelle de skriver med stor bokstav

        Lån nyttLån = new Lån();
        nyttLån.Bok = bok;
        nyttLån.LånsDato = DateOnly.FromDateTime(DateTime.Now);

        if (type == "s")
        {
            Console.Write("Skriv inn StudentID: ");
            int studentID = int.Parse(Console.ReadLine());
            Student student = studenter.FirstOrDefault(s => s.StudentID == studentID);
            if (student == null)
            {
                Console.WriteLine("Fant ikke studenten");
                return;
            }

            nyttLån.Låntaker = student.Navn;
        }
        else if (type == "a")
        {
            Console.Write("Skriv inn AnsattID: ");
            int ansattID = int.Parse(Console.ReadLine());
            Ansatt ansatt = ansatte.FirstOrDefault(a => a.AnsattID == ansattID);
            if (ansatt == null)
            { 
                Console.WriteLine("Fant ikke ansatt");
                return;
            }
            nyttLån.Låntaker = ansatt.Navn;
        }
        else
        {
            Console.WriteLine("Ugyldig valg.");
            return;
        }

        bok.TilgjengeligeEksemplarer--;
        låneHistorikk.Add(nyttLån);
        Console.WriteLine($"'{bok.Tittel}' er lånt ut til {nyttLån.Låntaker}!");

    }

    static void ReturnBok(List<Bøker> bøker, List<Student> studenter, List<Ansatt> ansatte, List<Lån> låneHistorikk)
    {
        Console.Write("Skriv inn BokID");
        int bokID = int.Parse(Console.ReadLine());

        Bøker bok = bøker.FirstOrDefault(b => b.BokID == bokID);
        if (bok == null)
        {
            Console.WriteLine("Fant ikke boken");
            return;
        }

        Lån aktiveLån = låneHistorikk.FirstOrDefault(l => l.Bok.BokID == bokID && l.ReturDato == null);
        if (aktiveLån == null)
        {
            Console.WriteLine("Denne boken har ingen aktive lån. ");
            return;
        }

        aktiveLån.ReturDato = DateOnly.FromDateTime(DateTime.Now);
        bok.TilgjengeligeEksemplarer++;
        Console.WriteLine($"'{bok.Tittel}'er returnert av {aktiveLån.Låntaker}");
    }

    static void RegistrerBok(List<Bøker> bøker)
    {
        Bøker nyBok = new Bøker();
        Console.Write("Tittel: ");
        nyBok.Tittel = Console.ReadLine();
        Console.Write("Forfatter: ");
        nyBok.Forfatter = Console.ReadLine();
        Console.Write("Utgivelseår: ");
        nyBok.UtgivelseÅr = int.Parse (Console.ReadLine());
        Console.Write("Antall eksemplarer: ");
        nyBok.Eksemplarer = int.Parse(Console.ReadLine());
        nyBok.TilgjengeligeEksemplarer = nyBok.Eksemplarer;
        nyBok.BokID = bøker.Count + 1;
        
        bøker.Add(nyBok);
        Console.WriteLine($"´{nyBok.Tittel}´ er registrert");
    }
    
    

    static void Avslutt()
    {
        Console.WriteLine("Ha en fin dag videre");
        Environment.Exit(0);
    }
}
//Klassene
class Student
{
    public int StudentID;
    public string Navn;
    public string Epost;
    public List<Kurs> Kurs = new List<Kurs>();
}
class Utvekslingstudent : Student
{
    public string Hjem_Universitet;
    public string Hjem_Land;
    public DateOnly Periode_start;
    public DateOnly Periode_slutt;
}

class Ansatt
{
    public int AnsattID;
    public string Navn;
    public string Epost;
    public string Stilling;
    public string Avdeling;
}
class Kurs
{
    public string KursKode;
    public int StudiePoeng;
    public string KursNavn;
    public int MaksPlasser;
    public List<Student> Deltakere = new List<Student>();
}

class Bøker
{
    public string Tittel;
    public string Forfatter;
    public int UtgivelseÅr;
    public int BokID;
    public int Eksemplarer;
    public int TilgjengeligeEksemplarer;
}

class Lån
{
    public string Låntaker;
    public Bøker Bok;
    public DateOnly LånsDato;
    public DateOnly? ReturDato;
}
