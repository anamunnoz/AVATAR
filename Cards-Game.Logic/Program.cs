using System.Collections;

namespace Cards_Game.Logic;

public class Program
{
    public static void Main(string[] args)
    {
        
        BaseDatos BaseDeDatos = new BaseDatos();

        /*
        Monstruo WindRunner = new Monstruo("Wind Runner", 150, 180, "Aire", "", 2, "../images/windrunner.jpg");
        BaseDeDatos.GuardarMonstruo(WindRunner);

        Monstruo EarthSpirit = new Monstruo("Earth Spirit", 200, 210, "Tierra", "", 4 , "../images /earthspirit.jpg");
        BaseDeDatos.GuardarMonstruo(EarthSpirit);
     

        Monstruo FireFly = new Monstruo("Fire Fly", 180, 230, "Fuego", "", 6, "../images /firefly.jpg");
        BaseDeDatos.GuardarMonstruo(FireFly);

        Monstruo Aquaman = new Monstruo("Aquaman", 250, 170, "Agua", "", 8, "../images /aquaman.jpg");
        BaseDeDatos.GuardarMonstruo(Aquaman);

        Monstruo Ember = new Monstruo("Ember", 180, 200, "Flama", "", 10, "../images /ember.jpg");
        BaseDeDatos.GuardarMonstruo(Ember);

        Monstruo Charizard = new Monstruo("Charizard", 150, 180, "Llamarada", "", 12, "../images /charizard.jpg");
        BaseDeDatos.GuardarMonstruo(Charizard);

        Monstruo Volcano = new Monstruo("Volcano", 140, 180, "Lava", "", 14, "../images /volcano.jpg");
        BaseDeDatos.GuardarMonstruo(Volcano);

        Monstruo Frost = new Monstruo("Frost", 190, 170, "Hielo", "", 16, "../images /frost.jpg");
        BaseDeDatos.GuardarMonstruo(Frost);

        Monstruo Bulbazor = new Monstruo("Bulbazor", 130, 150, "Planta", "", 18, "../images /bulbazor.jpg");
        BaseDeDatos.GuardarMonstruo(Bulbazor);

        Monstruo Eul = new Monstruo("Eul", 190, 200, "Ciclon", "", 20, "../images /eul.png");
        BaseDeDatos.GuardarMonstruo(Eul);

        Monstruo EarthShaker = new Monstruo("Earth Shaker", 230, 250, "Terremoto", "", 22, "../images /earthshaker.jpg");
        BaseDeDatos.GuardarMonstruo(EarthShaker);
        
        Monstruo Hydra = new Monstruo("Hydra", 200, 230, "Agua", "", 24, "../images/hydra.jpg");
        BaseDeDatos.GuardarMonstruo(Hydra);

        Monstruo ThunderCloud = new Monstruo("Thunder Cloud", 200, 230, "Ciclon", "", 26, "../images/thundercloud.jpg");
        BaseDeDatos.GuardarMonstruo(ThunderCloud);

        Monstruo Morphling = new Monstruo("Morphling", 200, 230, "Agua", "", 28, "../images/morphling.jpg");
        BaseDeDatos.GuardarMonstruo(Morphling);

        Monstruo Onix = new Monstruo("Onix", 200, 230, "Tierra", "", 30, "../images/onix.jpg");
        BaseDeDatos.GuardarMonstruo(Onix);

        Monstruo Etna = new Monstruo("Etna", 200, 230, "Lava", "", 32, "../images/etna.jpg");
        BaseDeDatos.GuardarMonstruo(Etna);

        Monstruo Flareon = new Monstruo("Flareon", 200, 230, "Flama", "", 34, "../images/flareon.jpg");
        BaseDeDatos.GuardarMonstruo(Flareon);

        Monstruo Venusaur = new Monstruo("Venusaur", 200, 230, "Planta", "", 36, "../images/venasaur.jpg");
        BaseDeDatos.GuardarMonstruo(Venusaur);

        Monstruo Slardar = new Monstruo("Slardar", 200, 230, "Agua", "", 38, "../images/slardar.jpg");
        BaseDeDatos.GuardarMonstruo(Slardar);

        Monstruo IceTitan = new Monstruo("IceTitan", 200, 230, "Hielo", " ", 40, "../images/icetitan.jpg");
        BaseDeDatos.GuardarMonstruo(IceTitan);

        Monstruo Katara = new Monstruo("Katara", 200, 230, "Agua", " ", 42, "../images/katara.jpg");
        BaseDeDatos.GuardarMonstruo(Katara);

        Monstruo Magma = new Monstruo("Magma", 200, 230, "Lava", " ", 44, "../images/magma.jpg");
        BaseDeDatos.GuardarMonstruo(Magma);

        Monstruo Tiny = new Monstruo("Tiny", 200, 230, "Tierra", " ", 46, "../images/tiny.jpg");
        BaseDeDatos.GuardarMonstruo(Tiny);

        Monstruo Iceberg = new Monstruo("Iceberg", 200, 230, "Hielo", " ", 48, "../images/iceberg.jpg");
        BaseDeDatos.GuardarMonstruo(Iceberg);

        Monstruo Leviatan = new Monstruo("Leviatan", 200, 230, "Agua", " ", 50, "../images/leviatan.png");
        BaseDeDatos.GuardarMonstruo(Leviatan);

        Monstruo Colosus = new Monstruo("Colosus", 200, 230, "Tierra", " ", 52, "../images/colosus.jpg");
        BaseDeDatos.GuardarMonstruo(Colosus);

        Monstruo Melt = new Monstruo("Melt", 200, 230, "Lava", " ", 54, "../images/melt.jpg");
        BaseDeDatos.GuardarMonstruo(Melt);
        
        Monstruo Melt = new Monstruo("Melt", 200, 230, "Llamarada", " ", 54, "../images/melt.jpg");
        BaseDeDatos.GuardarMonstruo(Melt);

        Monstruo Daytron = new Monstruo("Daytron", 200, 230, "Aire", "efecto", 56, "../images/daytron.jpg");
        BaseDeDatos.ExisteMonstruo(Daytron);

        Monstruo Pyke = new Monstruo("Pyke", 200, 230, "Aire", "efecto", 58, "../images/pyke.jpg");
        BaseDeDatos.GuardarMonstruo(Pyke);

        Monstruo Snap = new Monstruo("Snap", 200, 230, "Aire", "efecto", 60, "../images/snap.jpg");
        BaseDeDatos.GuardarMonstruo(Snap);

        Monstruo Enigma = new Monstruo("Enigma", 200, 230, "Aire", "efecto", 62, "../images/enigma.jpg");
        BaseDeDatos.GuardarMonstruo(Enigma);

        Monstruo Ventus = new Monstruo("Ventus", 200, 230, "Aire", "efecto", 64, "../images/ventus.jpg");
        BaseDeDatos.GuardarMonstruo(Ventus);

        Monstruo Bogna = new Monstruo("Bogna", 200, 230, "Tierra", "efecto", 66, "../images/bogna.jpg");
        BaseDeDatos.GuardarMonstruo(Bogna);

        Monstruo Saturno = new Monstruo("Saturno", 200, 230, "Tierra", "efecto", 68, "../images/saturno.jpg");
        BaseDeDatos.GuardarMonstruo(Saturno);

        Monstruo DawnBraker = new Monstruo("Dawn Braker", 200, 230, "Fuego", "efecto", 70, "../images/dawnbraker.jpg");
        BaseDeDatos.GuardarMonstruo(DawnBraker);

        Monstruo Nevermore = new Monstruo("Nevermore", 200, 230, "Fuego", "efecto", 72, "../images/nevermore.jpg");
        BaseDeDatos.GuardarMonstruo(Nevermore);

        Monstruo Doom = new Monstruo("Doom", 200, 230, "Fuego", "efecto", 74, "../images/doom.png");
        BaseDeDatos.GuardarMonstruo(Doom);

        Monstruo Lina = new Monstruo("Lina", 200, 230, "Fuego", "efecto", 76, "../images/lina.jpg");
        BaseDeDatos.GuardarMonstruo(Lina);

        Monstruo Meraxys = new Monstruo("Meraxys", 200, 230, "Fuego", "efecto", 78, "../images/meraxys.jpg");
        BaseDeDatos.GuardarMonstruo(Meraxys);

        Monstruo Balerion = new Monstruo("Balerion", 200, 230, "Flama", "efecto", 80, "../images/balerion.jpg");
        BaseDeDatos.GuardarMonstruo(Balerion);

        Monstruo Muk = new Monstruo("Muk", 200, 230, "Llamarada", "efecto", 82, "../images/muk.jpg");
        BaseDeDatos.GuardarMonstruo(Muk);

        Monstruo Outbreak = new Monstruo("Outbreak", 200, 230, "Planta", "efecto", 84, "../images/outbreak.png");
        BaseDeDatos.GuardarMonstruo(Outbreak);

        Monstruo CloudTail = new Monstruo("Cloud Tail", 200, 230, "Ciclon", "efecto", 86, "../images/cloudtail.jpg");
        BaseDeDatos.GuardarMonstruo(CloudTail);

        Monstruo Stun = new Monstruo("Stun", 200, 230, "Terremoto", "efecto", 88, "../images/stun.png");
        BaseDeDatos.GuardarMonstruo(Stun);

        Monstruo ScareCrow = new Monstruo("Scarecrow", 200, 230, "Terremoto", "efecto", 90, "../images/scarecrow.jpg");
        BaseDeDatos.GuardarMonstruo(ScareCrow);
        */


    }

    

    
}
