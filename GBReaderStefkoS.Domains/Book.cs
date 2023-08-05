using System.Reflection.Metadata.Ecma335;

namespace GBReaderStefkoS.Domains;

/**
 * Collectionne des objets pages
 * Accesseur pour les pages 
 * Permet de savoir si une page existe
 * Permet de savoir où on en est dans la lecture
 *
 * [ALGO] Type de collection
 * Nous n'avons pas besoin d'une association clé - valeur, la solution d'une map est donc retirée
 * Nous ne nous servironts pas d'un système de queue, cette collection est donc aussi retirée
 * Pas besoin d'ordonnée les valeurs, elles le seront deja
 *
 * Il reste donc la List et ses dérivées
 * Mon choix s'est porté sur la list car je vois devoir acceder souvent aux pages et la List à une CTT plus rapide 
 *
 * [ALGO] Implémentation de collection
 *
 * Il y a plusieurs choix possible LinkedList et la List
 *
 * La linkedList à une CTT plus rapide pour l'ajout et la suppression d'éléments ce qui n'est pas utile pour ce projet car
 * nous ne feront que de la lecture et de l'acces de pages
 *
 * La List à une CTT plus rapide pour l'acces aux éléments, ce qui est utile pour ce projet car nous allons souvent acceder aux pages O(1)
 *
 *
 * Mon choix se porte donc sur une List pour l'implementation de la collection
 *
 */

public record Book (Author Author, string Title, string Resume, string Isbn)
{

    public IList<Page> Pages { get; set; } = new List<Page>();

    public bool PageHaveChoice(int pageIndex)
    {
        return Pages.Any(p => p.Index == pageIndex && p.Choices.Count > 0);
    }

}