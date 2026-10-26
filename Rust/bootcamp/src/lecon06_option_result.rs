//! LEÇON 06 – Option et Result : gérer l'absence et les erreurs (pas de null, pas d'exceptions !)
//!
//! À retenir :
//!  - `Option<T>` : `Some(valeur)` ou `None` (absence de valeur).
//!  - `Result<T, E>` : `Ok(valeur)` ou `Err(erreur)`.
//!  - L'opérateur `?` : renvoie l'erreur immédiatement, sinon extrait la valeur.
//!  - Méthodes utiles : `unwrap_or(defaut)`, `map(...)`, `ok()`, `ok_or(...)`, `"42".parse::<i32>()`.

use std::num::ParseIntError;

/// Division : None si le diviseur vaut 0.
pub fn diviser(a: f64, b: f64) -> Option<f64> {
    todo!("à vous de jouer !")
}

/// Position du premier élément égal à `cible`, ou None.
pub fn trouver_position(valeurs: &[i32], cible: i32) -> Option<usize> {
    todo!("à vous de jouer !")
}

/// Convertit le texte en âge. Erreurs (message exact) :
///  - texte non numérique → Err("pas un nombre")
///  - âge > 150 → Err("âge invalide")
pub fn lire_age(texte: &str) -> Result<u8, String> {
    todo!("à vous de jouer !")
}

/// Additionne des nombres écrits en texte. Au premier texte invalide, renvoie l'erreur de parse.
/// Utilisez l'opérateur `?` !
pub fn somme_textes(textes: &[&str]) -> Result<i32, ParseIntError> {
    todo!("à vous de jouer !")
}

/// Renvoie le prénom en majuscules s'il existe, sinon "ANONYME".
pub fn prenom_ou_anonyme(prenom: Option<&str>) -> String {
    todo!("à vous de jouer !")
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_diviser() {
        assert_eq!(diviser(10.0, 4.0), Some(2.5));
        assert_eq!(diviser(1.0, 0.0), None);
    }

    #[test]
    fn test_trouver_position() {
        assert_eq!(trouver_position(&[4, 8, 15, 16], 15), Some(2));
        assert_eq!(trouver_position(&[4, 8], 42), None);
    }

    #[test]
    fn test_lire_age() {
        assert_eq!(lire_age("42"), Ok(42));
        assert_eq!(lire_age("abc"), Err("pas un nombre".to_string()));
        assert_eq!(lire_age("200"), Err("âge invalide".to_string()));
    }

    #[test]
    fn test_somme_textes() {
        assert_eq!(somme_textes(&["1", "2", "3"]), Ok(6));
        assert!(somme_textes(&["1", "deux", "3"]).is_err());
    }

    #[test]
    fn test_prenom_ou_anonyme() {
        assert_eq!(prenom_ou_anonyme(Some("Khaled")), "KHALED");
        assert_eq!(prenom_ou_anonyme(None), "ANONYME");
    }
}
