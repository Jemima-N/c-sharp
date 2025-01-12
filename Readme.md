Tetris Project Documentation
Structure du Projet
Le projet Tetris a été réalisé en utilisant WPF (Windows Presentation Foundation), une bibliothèque graphique de Microsoft pour les applications Windows. La structure de ce projet respecte les conventions de WPF.
Justification de l'Absence de Program.cs
Dans les projets WPF, il n'est pas nécessaire d'inclure un fichier Program.cs. Contrairement aux applications console ou WinForms, WPF utilise le fichier App.xaml et son code-behind App.xaml.cs pour définir le point d'entrée de l'application.
La méthode Main qui est généralement incluse dans un fichier Program.cs est automatiquement générée par WPF lors de la compilation. Cette méthode appelle la classe Application pour démarrer l'application graphique.
En résumé, dans les projets WPF :
•	Le point d'entrée de l'application est défini dans App.xaml.
•	Il n'est pas nécessaire d'avoir un fichier Program.cs car la méthode Main est générée automatiquement.
Justification du Choix de l'Interface WPF
Le projet utilise Windows Presentation Foundation (WPF) comme outil d'interface graphique pour plusieurs raisons :
1.	Modernité : WPF est une technologie moderne pour créer des interfaces utilisateur riches et dynamiques sur les applications Windows.
2.	Flexibilité : WPF permet de personnaliser facilement l'apparence de l'application à l'aide de XAML, offrant ainsi des possibilités avancées de design.
3.	Gestion des événements : WPF intègre une gestion puissante des événements, facilitant ainsi le développement des interactions utilisateur.
4.	Séparation du code et de la présentation : WPF suit le modèle MVVM (Model-View-ViewModel), ce qui permet de séparer la logique d'affaires de l'interface utilisateur, rendant le code plus propre et plus maintenable.
5.	Compatibilité : WPF est bien intégré dans l'écosystème .NET, ce qui garantit une bonne compatibilité avec les autres bibliothèques et outils Microsoft.
Ce choix permet de créer une application robuste, performante et visuellement attrayante.
Instructions pour Exécuter le Projet
1.	Assurez-vous que le SDK .NET 5.0 est installé sur votre machine.
2.	Ouvrez une console de commande dans le répertoire du projet.
3.	Exécutez la commande suivante pour lancer le jeu : 
4.	dotnet run --project JeuTetris\Tetris.csproj
Organisation des Fichiers
Pour respecter les bonnes pratiques de structuration des projets, les fichiers sont organisés comme suit :
•	Tous les fichiers liés à la logique du jeu sont regroupés dans le répertoire principal.
•	Les ressources telles que les images sont stockées dans le dossier Assets.
•	Le fichier README.md fournit une documentation complète sur le projet.
Cette organisation assure une bonne lisibilité et facilite la maintenance du projet.

