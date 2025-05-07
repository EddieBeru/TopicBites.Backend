using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicBites
{
    
    public class ChatGPT
    {
        static public StudyTopic CrearArbol()
        {
            // Raíz
            var root = new StudyTopic("Estudio de Especies");

            // Sección de Especies
            var especies = new StudyTopic("Especies");
            root.AddSubTopic(especies);

            // Especies principales
            var canino = new StudyTopic("Canino");
            var felino = new StudyTopic("Felino");
            var bovino = new StudyTopic("Bovino");

            especies.AddSubTopic(canino);
            especies.AddSubTopic(felino);
            especies.AddSubTopic(bovino);

            // Sistemas comunes
            var sistemaOseo = new StudyTopic("Sistema Óseo");
            var sistemaMuscular = new StudyTopic("Sistema Muscular");
            var sistemaNervioso = new StudyTopic("Sistema Nervioso");

            // Regiones comunes de Sistema Óseo
            var craneo = new StudyTopic("Cráneo");
            var columna = new StudyTopic("Columna Vertebral");
            var extremidades = new StudyTopic("Extremidades");

            // Regiones comunes de Sistema Muscular
            var musculosCabeza = new StudyTopic("Músculos de la Cabeza");
            var musculosTronco = new StudyTopic("Músculos del Tronco");

            // Regiones comunes de Sistema Nervioso
            var encefalo = new StudyTopic("Encéfalo");
            var medula = new StudyTopic("Médula Espinal");

            // Función para clonar sistemas
            StudyTopic ClonarSistema(string nombreSistema)
            {
                return new StudyTopic(nombreSistema);
            }

            // Función para clonar regiones dentro de sistemas
            void AgregarRegiones(StudyTopic sistema)
            {
                if (sistema.Title == "Sistema Óseo")
                {
                    sistema.AddSubTopic(new StudyTopic("Cráneo"));
                    sistema.AddSubTopic(new StudyTopic("Columna Vertebral"));
                    sistema.AddSubTopic(new StudyTopic("Extremidades"));
                }
                else if (sistema.Title == "Sistema Muscular")
                {
                    sistema.AddSubTopic(new StudyTopic("Músculos de la Cabeza"));
                    sistema.AddSubTopic(new StudyTopic("Músculos del Tronco"));
                }
                else if (sistema.Title == "Sistema Nervioso")
                {
                    sistema.AddSubTopic(new StudyTopic("Encéfalo"));
                    sistema.AddSubTopic(new StudyTopic("Médula Espinal"));
                }
            }

            // Asignar sistemas y regiones a cada especie
            foreach (var especie in new[] { canino, felino, bovino })
            {
                var oseo = ClonarSistema("Sistema Óseo");
                var muscular = ClonarSistema("Sistema Muscular");
                var nervioso = ClonarSistema("Sistema Nervioso");

                AgregarRegiones(oseo);
                AgregarRegiones(muscular);
                AgregarRegiones(nervioso);

                especie.AddSubTopic(oseo);
                especie.AddSubTopic(muscular);
                especie.AddSubTopic(nervioso);
            }

            return root;
        }
    }
    
}
