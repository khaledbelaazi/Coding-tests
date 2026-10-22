//! LEÇON 04 – Structures et méthodes
//!
//! À retenir :
//!  - `struct` regroupe des données ; `impl` lui ajoute des méthodes.
//!  - `fn new(...) -> Self` : constructeur par convention.
//!  - `&self` : lecture, `&mut self` : modification, `self` : prend possession de l'objet.
//!  - `#[derive(Debug, Clone, PartialEq)]` génère automatiquement affichage, copie et comparaison.

#[derive(Debug, Clone, PartialEq)]
pub struct Rectangle {
    pub largeur: f64,
    pub hauteur: f64,
}

impl Rectangle {
    pub fn new(largeur: f64, hauteur: f64) -> Self {
        todo!("à vous de jouer !")
    }

    /// Crée un carré (largeur = hauteur = côté).
    pub fn carre(cote: f64) -> Self {
        todo!("à vous de jouer !")
    }

    pub fn aire(&self) -> f64 {
        todo!("à vous de jouer !")
    }

    pub fn est_carre(&self) -> bool {
        todo!("à vous de jouer !")
    }

    /// true si `autre` tient entièrement dans ce rectangle (sans rotation).
    pub fn peut_contenir(&self, autre: &Rectangle) -> bool {
        todo!("à vous de jouer !")
    }

    /// Multiplie les dimensions par `facteur` (modifie le rectangle).
    pub fn agrandir(&mut self, facteur: f64) {
        todo!("à vous de jouer !")
    }
}

/// Un compte bancaire. Le solde est en centimes (i64) pour éviter les erreurs d'arrondi.
#[derive(Debug)]
pub struct Compte {
    pub titulaire: String,
    solde_centimes: i64,
}

impl Compte {
    pub fn new(titulaire: &str) -> Self {
        todo!("à vous de jouer !")
    }

    pub fn solde(&self) -> i64 {
        todo!("à vous de jouer !")
    }

    pub fn deposer(&mut self, centimes: i64) {
        todo!("à vous de jouer !")
    }

    /// Retire si possible et renvoie true ; renvoie false (sans rien changer) si le solde est insuffisant.
    pub fn retirer(&mut self, centimes: i64) -> bool {
        todo!("à vous de jouer !")
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_rectangle() {
        let r = Rectangle::new(3.0, 4.0);
        assert_eq!(r.aire(), 12.0);
        assert!(!r.est_carre());
        assert!(Rectangle::carre(5.0).est_carre());
    }

    #[test]
    fn test_peut_contenir() {
        let grand = Rectangle::new(10.0, 8.0);
        assert!(grand.peut_contenir(&Rectangle::new(5.0, 5.0)));
        assert!(!grand.peut_contenir(&Rectangle::new(11.0, 1.0)));
    }

    #[test]
    fn test_agrandir() {
        let mut r = Rectangle::new(2.0, 3.0);
        r.agrandir(2.0);
        assert_eq!(r, Rectangle::new(4.0, 6.0));
    }

    #[test]
    fn test_compte() {
        let mut c = Compte::new("Khaled");
        assert_eq!(c.titulaire, "Khaled");
        c.deposer(10_000);
        assert!(c.retirer(2_500));
        assert!(!c.retirer(1_000_000));
        assert_eq!(c.solde(), 7_500);
    }
}
