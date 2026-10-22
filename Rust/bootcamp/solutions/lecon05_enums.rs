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
        match self {
            Feu::Vert => Feu::Orange,
            Feu::Orange => Feu::Rouge,
            Feu::Rouge => Feu::Vert,
        }
    }

    /// Durée en secondes : Rouge 30, Orange 5, Vert 25.
    pub fn duree(self) -> u32 {
        match self {
            Feu::Rouge => 30,
            Feu::Orange => 5,
            Feu::Vert => 25,
        }
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
        match self {
            Forme::Cercle(r) => PI * r * r,
            Forme::Rectangle(l, h) => l * h,
            Forme::Triangle { base, hauteur } => base * hauteur / 2.0,
        }
    }

    pub fn nom(&self) -> &'static str {
        match self {
            Forme::Cercle(_) => "cercle",
            Forme::Rectangle(..) => "rectangle",
            Forme::Triangle { .. } => "triangle",
        }
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
    let morceaux: Vec<&str> = texte.split_whitespace().collect();
    match morceaux.as_slice() {
        ["avancer", n] => n.parse().ok().map(Commande::Avancer),
        ["tourner", "G"] => Some(Commande::Tourner('G')),
        ["tourner", "D"] => Some(Commande::Tourner('D')),
        ["stop"] => Some(Commande::Stop),
        _ => None,
    }
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
