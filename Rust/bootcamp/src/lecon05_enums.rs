//! LEÇON 05 – Énumérations et `match`
//!
//! À retenir :
//!  - Un `enum` liste les variantes possibles d'un type, et chaque variante peut porter des données.
//!  - `match` oblige à traiter TOUTES les variantes : le compilateur vous prévient si vous en oubliez une.
//!  - On peut déstructurer : `Forme::Rectangle(l, h) => l * h`

use std::f64::consts::PI;

#[derive(Debug, Clone, Copy, PartialEq)]
pub enum Feu {
    Rouge,
    Orange,
    Vert,
}

impl Feu {
    /// Couleur suivante : Vert → Orange → Rouge → Vert.
    pub fn suivant(self) -> Feu {
        todo!("à vous de jouer !")
    }

    /// Durée en secondes : Rouge 30, Orange 5, Vert 25.
    pub fn duree(self) -> u32 {
        todo!("à vous de jouer !")
    }
}

#[derive(Debug, Clone, PartialEq)]
pub enum Forme {
    Cercle(f64),                        // rayon
    Rectangle(f64, f64),                // largeur, hauteur
    Triangle { base: f64, hauteur: f64 },
}

impl Forme {
    pub fn aire(&self) -> f64 {
        todo!("à vous de jouer !")
    }

    pub fn nom(&self) -> &'static str {
        todo!("à vous de jouer !")
    }
}

#[derive(Debug, PartialEq)]
pub enum Commande {
    Avancer(u32),
    Tourner(char), // 'G' ou 'D'
    Stop,
}

/// Transforme un texte en commande : "avancer 10" → Avancer(10), "tourner G" → Tourner('G'),
/// "stop" → Stop, tout le reste → None.
/// Astuce : `let morceaux: Vec<&str> = texte.split_whitespace().collect();` puis `match morceaux.as_slice()`.
pub fn lire_commande(texte: &str) -> Option<Commande> {
    todo!("à vous de jouer !")
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_feu() {
        assert_eq!(Feu::Vert.suivant(), Feu::Orange);
        assert_eq!(Feu::Rouge.suivant(), Feu::Vert);
        let cycle: u32 = [Feu::Rouge, Feu::Orange, Feu::Vert].iter().map(|f| f.duree()).sum();
        assert_eq!(cycle, 60);
    }

    #[test]
    fn test_forme() {
        assert!((Forme::Cercle(1.0).aire() - PI).abs() < 1e-9);
        assert_eq!(Forme::Rectangle(3.0, 4.0).aire(), 12.0);
        assert_eq!(Forme::Triangle { base: 6.0, hauteur: 2.0 }.aire(), 6.0);
        assert_eq!(Forme::Rectangle(1.0, 1.0).nom(), "rectangle");
    }

    #[test]
    fn test_lire_commande() {
        assert_eq!(lire_commande("avancer 10"), Some(Commande::Avancer(10)));
        assert_eq!(lire_commande("tourner G"), Some(Commande::Tourner('G')));
        assert_eq!(lire_commande("stop"), Some(Commande::Stop));
        assert_eq!(lire_commande("avancer beaucoup"), None);
        assert_eq!(lire_commande("voler"), None);
    }
}
