//! LEÇON 08 – Traits : le polymorphisme en Rust
//!
//! À retenir :
//!  - Un `trait` définit des comportements communs (comme une interface en C#).
//!  - Un trait peut fournir une implémentation par défaut, que les types peuvent garder ou redéfinir.
//!  - `&dyn Animal` / `Box<dyn Animal>` : manipuler différents types via le même trait.
//!  - `impl fmt::Display for MonType` : permet `format!("{}", valeur)` et `.to_string()`.

use std::fmt;

pub trait Animal {
    fn nom(&self) -> String;
    fn cri(&self) -> String;

    /// Implémentation par défaut : "Je suis {nom} et je fais {cri}"
    fn presenter(&self) -> String {
        todo!("à vous de jouer !")
    }
}

pub struct Chien {
    pub nom: String,
}

pub struct Chat {
    pub nom: String,
}

impl Animal for Chien {
    fn nom(&self) -> String {
        todo!("à vous de jouer !")
    }

    /// Un chien fait "Wouf".
    fn cri(&self) -> String {
        todo!("à vous de jouer !")
    }
}

impl Animal for Chat {
    fn nom(&self) -> String {
        todo!("à vous de jouer !")
    }

    /// Un chat fait "Miaou".
    fn cri(&self) -> String {
        todo!("à vous de jouer !")
    }

    /// Le chat redéfinit presenter : "{nom} vous ignore."
    fn presenter(&self) -> String {
        todo!("à vous de jouer !")
    }
}

/// Renvoie la présentation de chaque animal, dans l'ordre.
pub fn concert(animaux: &[Box<dyn Animal>]) -> Vec<String> {
    todo!("à vous de jouer !")
}

#[derive(Debug, Clone, Copy, PartialEq)]
pub struct Point {
    pub x: i32,
    pub y: i32,
}

/// Affichage d'un point : "(3, -4)"
impl fmt::Display for Point {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        todo!("à vous de jouer !")
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_chien() {
        let rex = Chien { nom: "Rex".to_string() };
        assert_eq!(rex.cri(), "Wouf");
        assert_eq!(rex.presenter(), "Je suis Rex et je fais Wouf");
    }

    #[test]
    fn test_chat() {
        let felix = Chat { nom: "Félix".to_string() };
        assert_eq!(felix.cri(), "Miaou");
        assert_eq!(felix.presenter(), "Félix vous ignore.");
    }

    #[test]
    fn test_concert() {
        let animaux: Vec<Box<dyn Animal>> = vec![
            Box::new(Chien { nom: "Rex".to_string() }),
            Box::new(Chat { nom: "Félix".to_string() }),
        ];
        assert_eq!(concert(&animaux), vec!["Je suis Rex et je fais Wouf", "Félix vous ignore."]);
    }

    #[test]
    fn test_display_point() {
        assert_eq!(Point { x: 3, y: -4 }.to_string(), "(3, -4)");
    }
}
