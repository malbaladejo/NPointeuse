using NPointeuse.Infra.Client;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NPointeuse.Infra.XF
{
    public interface INavigationService
    {
        //
        // Résumé :
        //     Supprime de manière asynchrone le dernier Xamarin.Forms.Page dans la pile de
        //     navigation.
        //
        // Retourne :
        //     Xamarin.Forms.Page qui a été en haut de la pile de navigation.
        Task<Page> PopAsync();
        //
        // Résumé :
        //     Supprime de manière asynchrone le dernier Xamarin.Forms.Page dans la pile de
        //     navigation, avec une animation facultative.
        //
        // Paramètres :
        //   animated:
        //     Détermine s’il faut animer le dépilement.
        Task<Page> PopAsync(bool animated);
        //
        // Résumé :
        //     Ignore de manière asynchrone le dernier Xamarin.Forms.Page présenté sous forme
        //     modale.
        //
        // Retourne :
        //     Task<Page> pouvant être attendu, indiquant l’achèvement de PopModalAsync. Task.Result
        //     est la page qui a été dépilée.
        Task<Page> PopModalAsync();
        //
        // Résumé :
        //     Ignore de manière asynchrone le dernier Xamarin.Forms.Page présenté sous forme
        //     modale, avec une animation facultative.
        //
        // Paramètres :
        //   animated:
        //     Détermine s’il faut animer le dépilement.
        Task<Page> PopModalAsync(bool animated);
        //
        // Résumé :
        //     Dépile tout sauf le Xamarin.Forms.Page racine de la pile de navigation.
        //
        // Retourne :
        //     Tâche représentant l’opération de suppression asynchrone.
        Task PopToRootAsync();
        //
        // Résumé :
        //     Dépile tout sauf le Xamarin.Forms.Page racine de la pile de navigation, avec
        //     une animation facultative.
        //
        // Paramètres :
        //   animated:
        //     Détermine s’il faut animer le dépilement.
        Task PopToRootAsync(bool animated);
        //
        // Résumé :
        //     Ajoute de manière asynchrone un Xamarin.Forms.Page en haut de la pile de navigation.
        //
        // Paramètres :
        //   page:
        //     Xamarin.Forms.Page à pousser en haut de la pile de navigation.
        //
        // Retourne :
        //     Tâche qui représente l’opération de poussée asynchrone.
        Task PushAsync(INavigationToken token);
        //
        // Résumé :
        //     Ajoute de manière asynchrone un Xamarin.Forms.Page en haut de la pile de navigation,
        //     avec une animation facultative.
        //
        // Paramètres :
        //   page:
        //     Page à pousser.
        //
        //   animated:
        //     Détermine s’il faut animer la poussée.
        Task PushAsync(INavigationToken token, bool animated);
        //
        // Résumé :
        //     Présente un Xamarin.Forms.Page sous forme modale.
        //
        // Paramètres :
        //   page:
        //     Xamarin.Forms.Page à présenter sous forme modale.
        //
        // Retourne :
        //     Tâche pouvant être attendue qui indique l’achèvement de PushModal.
        Task PushModalAsync(INavigationToken token);
        //
        // Résumé :
        //     Présente un Xamarin.Forms.Page sous forme modale, avec une animation facultative.
        //
        // Paramètres :
        //   page:
        //     Page à pousser.
        //
        //   animated:
        //     Détermine s’il faut animer la poussée.
        Task PushModalAsync(INavigationToken token, bool animated);
        //
        // Résumé :
        //     Supprime la page spécifiée de la pile de navigation.
        //
        // Paramètres :
        //   page:
        //     Page à supprimer.
        //void RemovePage(INavigationToken token);
    }
}
