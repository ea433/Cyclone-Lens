using Models.Classes;
using Logic.Enums;


namespace CycloneLensTestProject.ModelTests
{
    public class CycloonOverzichtTests
        {
            [Fact]
            public void Constructor_SetsPropertiesCorrectly()
            {
                // Arrange
                int id = 1;
                string naam = "Maria";
                CategorieType categorie = CategorieType.Categorie_1; 
                BassinType bassin = BassinType.Noord_Atlantisch; 
                StatusType status = StatusType.Actief; 

                // Act
                var cycloon = new CycloonOverzicht(id, naam, categorie, bassin, status);

                // Assert
                Assert.Equal(id, cycloon.Id);
                Assert.Equal(naam, cycloon.Naam);
                Assert.Equal(categorie, cycloon.Categorie);
                Assert.Equal(bassin, cycloon.Bassin);
                Assert.Equal(status, cycloon.Status);
            }
        }
    }

