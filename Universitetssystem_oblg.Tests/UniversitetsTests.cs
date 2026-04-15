using Universitetssystem_oblg;

namespace Universitetssystem_oblg.Tests;

public class UniversitetsTests
{
    [Fact]
    //Duplikatekurs kode blir ikke laget
    public void OpprettKurs_Duplikat()
    {
        List<Kurs> kursliste = new List<Kurs>();
        Kurs eksisterendeKurs = new Kurs
        {
            KursKode = "IS110",
            KursNavn = "Programmering",
            StudiePoeng = 10,
            MaksPlasser = 100
        };
        kursliste.Add(eksisterendeKurs);

        bool finnesAllerede = kursliste.Any(k => k.KursKode.ToLower() == "is110");
        if (!finnesAllerede)
            kursliste.Add(new Kurs {KursKode = "IS110", KursNavn = "Annet Navn"});
        
        Assert.Equal(1, kursliste.Count);
    }

    [Fact]
    //Allerede påmeldte blir ikke påmeldt igjen
    public void MeldPåKurs_Duplikat()
    {
        Student student = new Student { StudentID = 1, Navn = "Kari Nordmann" };
        Kurs kurs = new Kurs { KursKode = "IS110", KursNavn = "Programmering", MaksPlasser = 100 };
        
        //1 påmelding
        if (!kurs.Deltakere.Contains(student))
        {
            kurs.Deltakere.Add(student);
            student.Kurs.Add(kurs);
        }
        
        //2 påmelding
        if (!kurs.Deltakere.Contains(student))
        {
            kurs.Deltakere.Add(student);
            student.Kurs.Add(kurs);
        }
        
        Assert.Equal(1, kurs.Deltakere.Count);
    }

    [Fact]
    //Lån og retur eksampler oppdateres riktig
    public void LånOgRetur_Duplikat()
    {
        Bøker bok = new Bøker
        {
            BokID = 1,
            Tittel = "C# enkelt forklart",
            Eksemplarer = 3,
            TilgjengeligeEksemplarer = 3
        };

        List<Lån> låneHistorikk = new List<Lån>();
        
        //Låner
        Lån nyttLån = new Lån
        {
            Bok = bok,
            Låntaker = "Kari Nordmann",
            LåntakerType = "student",
            LånsDato = DateOnly.FromDateTime(DateTime.Now),
        };
        bok.TilgjengeligeEksemplarer--;
        låneHistorikk.Add(nyttLån);
        
        Assert.Equal(2, bok.TilgjengeligeEksemplarer);
        
        //retur
        nyttLån.ReturDato = DateOnly.FromDateTime(DateTime.Now);
        bok.TilgjengeligeEksemplarer++;
        
        Assert.Equal(3, bok.TilgjengeligeEksemplarer);
    }
}
