//! LEÇON 03 – Possession (ownership) et emprunts (borrowing) : LA notion clé de Rust
//!
//! À retenir :
//!  - Chaque valeur a UN seul propriétaire. Quand il sort de portée, la valeur est libérée.
//!  - `let b = a;` avec une String : la valeur est DÉPLACÉE, `a` n'est plus utilisable.
//!  - `&x` : emprunt en lecture (autant qu'on veut). `&mut x` : emprunt en écriture (un seul à la fois).
//!  - `&str` est une « vue » sur du texte ; `String` possède son texte. `s.as_str()`, `s.to_string()`.
//!  - Durées de vie `'a` : indiquent que la référence renvoyée vit aussi longtemps que les paramètres.

/// Renvoie le nombre de caractères du texte (emprunt en lecture). Attention : "é" = 1 caractère.
/// Astuce : `texte.chars().count()`.
pub fn nombre_de_caracteres(texte: &str) -> usize {
    todo!("à vous de jouer !")
}

/// Ajoute " !" à la fin du texte, SANS le renvoyer : on le modifie via l'emprunt mutable.
pub fn ajouter_exclamation(texte: &mut String) {
    todo!("à vous de jouer !")
}

/// Prend possession de la String et renvoie une nouvelle String en majuscules.
pub fn crier(texte: String) -> String {
    todo!("à vous de jouer !")
}

/// Renvoie le premier mot d'une phrase (avant le premier espace), sans copier le texte.
/// "bonjour le monde" → "bonjour" ; "seul" → "seul".
pub fn premier_mot(phrase: &str) -> &str {
    todo!("à vous de jouer !")
}

/// Renvoie la plus longue des deux chaînes (la première en cas d'égalité).
pub fn plus_long<'a>(a: &'a str, b: &'a str) -> &'a str {
    todo!("à vous de jouer !")
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_nombre_de_caracteres() {
        assert_eq!(nombre_de_caracteres("Rust"), 4);
        assert_eq!(nombre_de_caracteres("été"), 3);
    }

    #[test]
    fn test_ajouter_exclamation() {
        let mut s = String::from("Bonjour");
        ajouter_exclamation(&mut s);
        assert_eq!(s, "Bonjour !");
    }

    #[test]
    fn test_crier() {
        let s = String::from("vite");
        let resultat = crier(s);
        // s n'est plus utilisable ici : elle a été déplacée dans crier()
        assert_eq!(resultat, "VITE");
    }

    #[test]
    fn test_premier_mot() {
        assert_eq!(premier_mot("bonjour le monde"), "bonjour");
        assert_eq!(premier_mot("seul"), "seul");
    }

    #[test]
    fn test_plus_long() {
        assert_eq!(plus_long("chat", "éléphant"), "éléphant");
        assert_eq!(plus_long("abc", "xyz"), "abc");
    }
}
