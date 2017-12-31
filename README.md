# Unity_Workshop_Enjmin_2017
A Unity workshop, where I discover the joys of Editor Scripting.

Inspiré des deux premiers métriques du Game Feel (Steve Swink), l’input et la response, cet outil permet d’assigner aux inputs (du joueur sur le contrôleur) des courbes de réponses ADSR (attack, sustain, decay, release). 

Il s’agissait pour moi de découvrir l’editor scripting de Unity, et de me familiariser avec les custom editor, les custom property drawer, etc. J’avais initialement prévu de pouvoir gérer tous les types d’input et toutes les combinaisons possibles (axes pour un mouvement combiné avec un bouton de saut par exemple), mais l’editor scripting s’est avéré plus difficile à cerner que ce que je pensais... 

Le rendu du 31/12/2017 comprend donc un seul preset, d’un mouvement d’avatar (ou de camera). Il est possible de définir plusieurs courbes de réponse associées par exemple à différents types de terrains (terre, herbe, glace...). 
Chaque courbe définit l’attaque de la réponse (en combien de temps l’avatar atteint sa vitesse maximale) et sa phase de release (en combien de temps après avoir arrêté l’input la vitesse de l’avatar redescend à 0), ainsi qu’une vitesse maximale (sous forme de facteur de vitesse appliqué à la courbe) et une éventuelle courbe de transition in. 
Il est ainsi facile de paramétrer plusieurs états ou terrains pour que l’avatar se déplace de manière différente selon qu’il est sur la glace, sur un sol dur/mou, en l’air (pendant un saut). L’outil s’applique que le jeu soit en 2D ou en 3D, il est possible de choisir les axes du mouvement. De même, tous les inputs (Clavier, manette, souris..) sont possibles, il suffit de le préciser dans le script de contrôle du mouvement.
