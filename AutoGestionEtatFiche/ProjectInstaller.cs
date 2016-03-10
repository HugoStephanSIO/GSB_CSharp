using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGestionEtatFiche
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        /// <summary>
        /// Installateur par défaut pour le service AutoGestionEtatFiche.exe
        /// Commandes à utiliser depuis la console développement Visual Studio lancée en admin :
        /// >> cd /Projets_Home/AutoGestionEtatFiches/bin/debug
        /// >> installutil.exe AutoGestionEtatFiches.exe
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
