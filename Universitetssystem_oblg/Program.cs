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
        
       
        LoggInn innloggetBruker = InnloggingEllerRegistrering(studenter, ansatte);

        if (innloggetBruker is Student student)
        {
            StudentMeny(student, kursliste, studenter, bøker, låneHistorikk);
        }
        else if (innloggetBruker is Ansatt ansatt)
        {
            if (ansatt.Rolle == "faglærer")
                FaglærerMeny(ansatt, kursliste, bøker, låneHistorikk);
            else if (ansatt.Rolle == "bibliotekar")
                BibliotekarMeny(ansatt, bøker, låneHistorikk);
        }
    }

    static void StudentMeny(Student student, List<Kurs> kursliste, List<Student> studenter, List<Bøker> bøker, List<Lån> lånehistorikk)
    {
        bool startup = true;
        while (startup)
        {
            Console.WriteLine($"---Student Meny, {student.Navn} ---");
            Console.WriteLine("[1] Meld på/av kurs");
            Console.WriteLine("[2] Se mine kurs");
            Console.WriteLine("[3] Se mine karakterer");
            Console.WriteLine("[4] Søk på bok");
            Console.WriteLine("[5] Lån bok");
            Console.WriteLine("[6] Returner bok");
            Console.WriteLine("[0] Logg ut");
            Console.Write("Valg: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1": MeldPåKurs(kursliste, student); break;
                case "2": SeEgneKurs(student); break;
                case "3": SeKarakterer(student, kursliste); break;
                case "4": SøkPåBok(bøker); break;
                case "5": Lånebok(bøker, student, lånehistorikk); break;
                case "6": ReturnBok(bøker, student, lånehistorikk); break;
                case "0": startup = false; break;
                default: Console.WriteLine("Ugyldig valg"); break;
            }

        }
    }

    static void FaglærerMeny(Ansatt faglærer, List<Kurs> kursliste, List<Bøker> bøker, List<Lån> låneHistorikk)
    {
        bool startup = true;
        while (startup)
        {
            Console.WriteLine($"---Faglærer menyen, {faglærer.Navn} ---");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Søk på kurs");
            Console.WriteLine("[3] Søk på bok");
            Console.WriteLine("[4] Returner bok");
            Console.WriteLine("[5] Lån bok");
            Console.WriteLine("[6] Sett karakterer");
            Console.WriteLine("[7] Registrer pensum");
            Console.WriteLine("[0] Logg ut");
            Console.Write("Valg: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1": OpprettKurs(kursliste, faglærer); break;
                case "2": SøkPåKurs(kursliste); break;
                case "3": SøkPåBok(bøker); break;
                case "4": Lånebok(bøker, faglærer, låneHistorikk); break;
                case "5": ReturnBok(bøker, faglærer, låneHistorikk); break;
                case "6": SettKarakter(kursliste, faglærer); break;
                case "7": RegistrerPensum(kursliste, bøker, faglærer); break;
                case "0": startup = false; break;
                default: Console.WriteLine("Ugyldig valg"); break;
            }
        }
    }

    static void BibliotekarMeny(Ansatt bibliotekar, List<Bøker> bøker, List<Lån> låneHistorikk)
    {
        bool startup = true;
        while (startup)
        {
            Console.WriteLine($"---Bibliotek menyen, {bibliotekar.Navn} ---");
            Console.WriteLine("[1] Registrer bok");
            Console.WriteLine("[2] Se aktive lån");
            Console.WriteLine("[3] Se lånehistorikk");
            Console.WriteLine("[0] Logg ut");
            Console.Write("Valg: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1": RegistrerBok(bøker); break;
                case "2": SeAktiveLån(låneHistorikk); break;
                case "3": SeHistorikk(låneHistorikk); break;
                case "0": startup = false; break;
                default: Console.WriteLine("Ugyldig valg"); break;
            }
        }
    }

    //Logg inn
    static LoggInn InnloggingEllerRegistrering(List<Student> studenter, List<Ansatt> ansatte)
    {
        while (true)
        {
            Console.WriteLine("----- Velkommen -----");
            Console.WriteLine("[1] Logg inn");
            Console.WriteLine("[2] Registrer ny bruker");
            Console.Write("Valg: ");
            string valg = Console.ReadLine();

            if (valg == "1")
            {
                LoggInn bruker = LoggInn(studenter, ansatte);
                if (bruker != null) return bruker;
            }
            else if (valg == "2")
            {
                Registrer(studenter, ansatte);
            }
            else
            {
                Console.WriteLine("Ugyldig valg skriv inn enten 1 for logg inn eller 2 for registrering.");
            }
        }
    }
    
    static LoggInn LoggInn(List<Student> studenter, List<Ansatt> ansatte)
    {
        Console.Write("Brukernavn: ");
        string brukernavn = Console.ReadLine();
        Console.Write("Passord: ");
        string passord = Console.ReadLine();

        Student student = studenter.FirstOrDefault(s => s.BrukerNavn == brukernavn && s.Passord == passord);
        if (student != null)
        {
            Console.WriteLine($"Velkommen, {student.Navn}! (Student)");
            return student;
        }

        Ansatt ansatt = ansatte.FirstOrDefault(a => a.BrukerNavn == brukernavn);
        if (ansatt != null)
        {
            Console.WriteLine($"Velkommen, {ansatt.Navn}! (Ansatt)");
            return ansatt;
        }

        Console.WriteLine("Feil brukernavn eller passord. Prøv igjen");
        return null;
    }

    static void Registrer(List<Student> studenter, List<Ansatt> ansatte)
    {
        Console.WriteLine("---- Registrering ----");
        Console.WriteLine("[1] Student");
        Console.WriteLine("[2] Faglærer");
        Console.WriteLine("[3] Bibliotekar");
        Console.Write("Valg: ");
        string valg = Console.ReadLine();

        Console.Write("Navn: ");
        string navn = Console.ReadLine();
        Console.Write("Epost: ");
        string epost = Console.ReadLine();
        Console.Write("Brukernavn: ");
        string brukernavn = Console.ReadLine();

        bool brukernavnTatt =
            studenter.Any(s => s.BrukerNavn == brukernavn) ||
            ansatte.Any(a => a.BrukerNavn == brukernavn);
        
        if (brukernavnTatt)
        {
            Console.WriteLine("Brukernavn er allerede i bruke. Vennligst skriv inn et annet brukernavn");
            return;
        }

        Console.Write("Passord: ");
        string passord = Console.ReadLine();

        if (valg == "1")
        {
            Student nyStudent = new Student
            {
                StudentID = studenter.Count + 1,
                Navn = navn,
                Epost = epost,
                BrukerNavn = brukernavn,
                Passord = passord

            };
            studenter.Add(nyStudent);
            Console.WriteLine($"Student '{navn}' er registrert! Du kan nå logge inn.");

        }

        else if (valg == "2")
        {
            Ansatt nyFaglærer = new Ansatt
            {
                AnsattID = ansatte.Count + 1,
                Navn = navn,
                Epost = epost,
                BrukerNavn = brukernavn,
                Passord = passord,
                Stilling = "Faglærer",
                Rolle = "faglærer"
            };
            ansatte.Add(nyFaglærer);
            Console.WriteLine($"Faglærer '{navn}' er registrert! Du kan nå logge inn.");

        }
        else if (valg == "3")
        {
            Ansatt nyBibliotekar = new Ansatt
            {
                AnsattID = ansatte.Count + 1,
                Navn = navn,
                Epost = epost,
                BrukerNavn = brukernavn,
                Passord = passord,
                Stilling = "Bibliotekar",
                Rolle = "bibliotekar"
            };
            ansatte.Add(nyBibliotekar);
            Console.WriteLine($"Faglærer '{navn}' er registert! Du kan nå logge inn. ");


        }
        else
        {
            Console.WriteLine("Ugyldig valg. Vennligst velg 1, 2 eller 3");
        }
    }


//Metodene
    static void OpprettKurs(List<Kurs> kursliste, Ansatt faglærer)
    {
        Console.WriteLine("---- Opprett Kurs ----");
        Console.WriteLine("Vennligst skriv inn følgende for å opprette kurs");
        Console.Write("Kurskode: ");
        string kurskode = Console.ReadLine();

        bool finnesAllerede = kursliste.Any(k => k.KursKode.ToLower() == kurskode.ToLower());
        if (finnesAllerede)
        {
            Console.WriteLine("Et kurs med denne kurskoden eksisterer allerede. ");
            return;
        }

        Console.Write("Kursnavn: ");
        string kursnavn = Console.ReadLine();

        bool navnFinnes = kursliste.Any(k => k.KursNavn.ToLower() == kursnavn.ToLower());
        if (navnFinnes)
        {
            Console.WriteLine("Et kurs med dette navnet eksisterer allerede. ");
            return;
        }

        Console.Write("Studiepoeng: ");
        if (!int.TryParse(Console.ReadLine(), out int studiepoeng))
        {
            Console.WriteLine("Ugyldig verdi for studiepoeng");
            return;
        }

        Console.Write("Maks plasser: ");
        if (!int.TryParse(Console.ReadLine(), out int maksPlasser))
        {
            Console.WriteLine("Ugyldig verdi for maksplasser");
            return;
        }

        Kurs nyttKurs = new Kurs
        {
            KursKode = kurskode,
            KursNavn = kursnavn,
            StudiePoeng = studiepoeng,
            MaksPlasser = maksPlasser,
            Faglærer = faglærer
        };

        kursliste.Add(nyttKurs);
        Console.WriteLine($"Kurset {nyttKurs.KursNavn} er opprettet ");
    }

    static void MeldPåKurs(List<Kurs> kursliste, Student student)
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
            
            if (kurs.Deltakere.Contains(student))
            {
                Console.WriteLine("Du er allerede meldt på dette kurset. ");
                return;
            }

            kurs.Deltakere.Add(student);
            student.Kurs.Add(kurs);
            Console.WriteLine($"{student.Navn} er meldt på {kurs.KursNavn}!");
        }
        else if (valg == "2")
        {
            if (!kurs.Deltakere.Contains(student))
            {
                Console.WriteLine("Du er ikke meldt på dette kurset. ");
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

    static void Lånebok(List<Bøker> bøker, LoggInn bruker, List<Lån> låneHistorikk)
    {
        Console.Write("Skriv inn BokID");
        if (!int.TryParse(Console.ReadLine(), out int bokID))
        {
            Console.WriteLine("Ugyldig BokID. ");
            return;
        }

        Bøker bok = bøker.FirstOrDefault(b => b.BokID == bokID);
        if (bok == null)
        {
            Console.WriteLine("Fant ikke boken. ");
        }

        if (bok.TilgjengeligeEksemplarer == 0)
        {
            Console.WriteLine("Ingen tilgjengelige eksemplarer");
            return;
        }

        Lån nyttLån = new Lån
        {
            Bok = bok,
            LånsDato = DateOnly.FromDateTime(DateTime.Now),
        };

        if (bruker is Student student)
        {
            nyttLån.Låntaker = student.Navn;
            nyttLån.LåntakerType = "student";
            student.AktiveLån.Add(nyttLån);
        }
        else if (bruker is Ansatt ansatt)
        {
            nyttLån.Låntaker = ansatt.Navn;
            nyttLån.LåntakerType = "ansatt";
            ansatt.AktiveLån.Add(nyttLån);
        }

        bok.TilgjengeligeEksemplarer--;
        låneHistorikk.Add(nyttLån);
        Console.WriteLine($"'{bok.Tittel}' er lånt ut til {nyttLån.Låntaker}!");
    }


    static void ReturnBok(List<Bøker> bøker, LoggInn bruker, List<Lån> låneHistorikk)
    {
        Console.Write("Skriv inn BokID");
        if (!int.TryParse(Console.ReadLine(), out int bokID))
        {
            Console.WriteLine("Ugyldig BokID. ");
        }

        Bøker bok = bøker.FirstOrDefault(b => b.BokID == bokID);
        if (bok == null)
        {
            Console.WriteLine("Fant ikke boken. ");
            return;
        }

        string lånetakerNavn = bruker is Student s ? s.Navn : ((Ansatt)bruker).Navn;

        Lån aktivtLån = låneHistorikk.FirstOrDefault(l =>
            l.Bok.BokID == bokID &&
            l.Låntaker == lånetakerNavn &&
            l.ReturDato == null);

        if (aktivtLån == null)
        {
            Console.WriteLine("Du har ikke lånt denne boken. ");
            return;
        }

        aktivtLån.ReturDato = DateOnly.FromDateTime(DateTime.Now);
        bok.TilgjengeligeEksemplarer++;

        if (bruker is Student student)
            student.AktiveLån.Remove(aktivtLån);
        else if (bruker is Ansatt ansatt)
            ansatt.AktiveLån.Remove(aktivtLån);
    }

    static void RegistrerBok(List<Bøker> bøker)
    {
        Console.WriteLine("---- Registrer bok ----");
        Console.WriteLine("Vennligst fyll inn informasjon under for å registrere bok");
        Console.Write("Tittel:");
        string tittel = Console.ReadLine();
        Console.Write("Forfatter:");
        string forfatter = Console.ReadLine();

        Console.Write("Utgivelseår: ");
        if (!int.TryParse(Console.ReadLine(), out int år))
        {
            Console.WriteLine("Ugyldig årstall. ");
            return;
        }

        Console.Write("Antall eskemplarer: ");
        if (!int.TryParse(Console.ReadLine(), out int eksemplarer))
        {
            Console.WriteLine("Ugyldig antall");
            return;
        }

        Bøker nyBok = new Bøker
        {
            Tittel = tittel,
            Forfatter = forfatter,
            UtgivelseÅr = år,
            Eksemplarer = eksemplarer,
            TilgjengeligeEksemplarer = eksemplarer,
            BokID = bøker.Count + 1
        };

        bøker.Add(nyBok);
        Console.WriteLine($"{nyBok.Tittel} er registrert");
    }
//Student metoder nye
    static void SeEgneKurs(Student student)
    {
        Console.WriteLine("----Oversikt over dine kurs----");

        if (student.Kurs.Count == 0)
        {
            Console.WriteLine("Du er ikke meldt på noen kurs. ");
            return;
        }

        Console.WriteLine("--------------------------------");
        foreach (Kurs kurs in student.Kurs)
        {
            Console.WriteLine($"[{kurs.KursKode}] {kurs.KursNavn} - {kurs.StudiePoeng} studiepoeng");
        }
    }

    static void SeKarakterer(Student student, List<Kurs> kursliste)
    {
        Console.WriteLine("---- Dine karakterer ----");

        bool harKarakterer = false;
        foreach (Kurs kurs in kursliste)
        {
            if (kurs.Karakterer.ContainsKey(student.StudentID))
            {
                Console.WriteLine($"[{kurs.KursKode}] {kurs.KursNavn}: {kurs.Karakterer[student.StudentID]}");
                harKarakterer = true;
            }
        }

        if (!harKarakterer)
            Console.WriteLine("Du har ingen karakterer enda. ");
    }

//Faglærer metoder nye
    static void SettKarakter(List<Kurs> kursliste, Ansatt Faglærer)
    {
        List<Kurs> mineKurs = kursliste.Where(k => k.Faglærer == Faglærer).ToList();
        if (mineKurs.Count == 0)
        {
            Console.WriteLine("Du underviser ingen kurs");
            return;
        }

        Console.WriteLine("---- Dine kurs ----");
        foreach (Kurs kurs in mineKurs)
            Console.WriteLine($"[{kurs.KursKode}] {kurs.KursNavn}");

        Console.Write("Skriv inn kurskode: ");
        string kurskode = Console.ReadLine().ToLower();

        Kurs valgtKurs = mineKurs.FirstOrDefault(k => k.KursKode.ToLower() == kurskode);
        if (valgtKurs == null)
        {
            Console.WriteLine("Fant ikke kurset, eller du underviser ikke dette kurset");
            return;
        }

        if (valgtKurs.Deltakere.Count == 0)
        {
            Console.WriteLine("Ingen studenter er meldt på dette kurset. ");
            return;
        }

        Console.WriteLine("Studenter i kurset: ");
        foreach (Student student in valgtKurs.Deltakere)
            Console.WriteLine($"[{student.StudentID}] {student.Navn}");

        Console.Write("Skriv inn studentID: ");
        if (!int.TryParse(Console.ReadLine(), out int studentID))
        {
            Console.WriteLine("Ugyldig student ID");
            return;
        }

        Student valgtStudent = valgtKurs.Deltakere.FirstOrDefault(s => s.StudentID == studentID);
        if (valgtStudent == null)
        {
            Console.WriteLine("Fant ikke studenten i dette kurset. ");
            return;
        }

        Console.Write("Skriv inn karakter (A/B/C/D/E/F): ");
        string karakter = Console.ReadLine().ToUpper();

        string[] gyldigeKarakterer = { "A", "B", "C", "D", "E", "F" };
        if (!gyldigeKarakterer.Contains(karakter))
        {
            Console.WriteLine("Ugyldige karakter. Må være A, B, C, D, E eller F");
            return;
        }

        valgtKurs.Karakterer[studentID] = karakter;
        Console.WriteLine($"Karakter '{karakter}' er satt for {valgtStudent.Navn} i {valgtKurs.KursNavn}. ");
    }

    static void RegistrerPensum(List<Kurs> kursliste, List<Bøker> bøker, Ansatt faglærer)
    {
        List<Kurs> mineKurs = kursliste.Where(k => k.Faglærer == faglærer).ToList();
        if (mineKurs.Count == 0)
        {
            Console.WriteLine("Du underviser ingen kurs");
        }


        Console.WriteLine("----Dine kurs----");
        foreach (Kurs kurs in mineKurs)
            Console.WriteLine($"[{kurs.KursKode}] {kurs.KursNavn}");

        Console.Write("Skriv inn kurskode: ");
        string kurskode = Console.ReadLine().ToLower();

        Kurs valgtKurs = mineKurs.FirstOrDefault(k => k.KursKode.ToLower() == kurskode);
        if (valgtKurs == null)
        {
            Console.WriteLine("Fant ikke kurset, eller du underviser ikke dette kurset. ");
            return;
        }

        if (bøker.Count == 0)
        {
            Console.WriteLine("Ingen bøker er registrert i systemer. ");
            return;
        }

        Console.WriteLine("Tilgjengelige bøker: ");
        foreach (Bøker bok in bøker)
            Console.WriteLine($"[{bok.BokID}] {bok.Tittel} - {bok.Forfatter}");

        Console.Write("Skriv inn BokID: ");
        if (!int.TryParse(Console.ReadLine(), out int bokID))
        {
            Console.WriteLine("Ugyldig BokID");
            return;
        }

        Bøker valgtBok = bøker.FirstOrDefault(b => b.BokID == bokID);
        if (valgtBok == null)
        {
            Console.WriteLine("Fant ikke boken. ");
            return;
        }

        if (valgtKurs.Pensum.Contains(valgtBok))
        {
            Console.WriteLine("Denne boken er allerede lagt til i pensum. ");
            return;
        }

        valgtKurs.Pensum.Add(valgtBok);
        Console.WriteLine($"{valgtBok.Tittel} er lagt til i pensum for {valgtKurs.KursNavn}");
    }

//Bibliotekar metoder nye
    static void SeAktiveLån(List<Lån> låneHistorikk)
    {
        List<Lån> aktiveLån = låneHistorikk.Where(l => l.ReturDato == null).ToList();

        if (aktiveLån.Count == 0)
        {
            Console.WriteLine("Ingen aktive lån");
        }

        Console.WriteLine("---- Aktive lån ----");
        foreach (Lån lån in aktiveLån)
        {
            Console.WriteLine($"{lån.Bok.Tittel} - lånt av {lån.Låntaker}({lån.LåntakerType}) den {lån.LånsDato}");
        }
    }

    static void SeHistorikk(List<Lån> låneHistorikk)
    {
        if (låneHistorikk.Count == 0)
        {
            Console.WriteLine("Ingen lånehistorikk");
            return;
        }


        Console.WriteLine("----Låne historikk ----");
        foreach (Lån lån in låneHistorikk)
        {
            string status = lån.ReturDato == null ? "Ikke returner" : $"Returnert {lån.ReturDato}";
            Console.WriteLine($"{lån.Bok.Tittel} - {lån.Låntaker} ({lån.LåntakerType}) - lånt {lån.LånsDato} - {status}");
        }
    }




    static void Avslutt()
    {
        Console.WriteLine("Ha en fin dag videre");
        Environment.Exit(0);
    }
}
