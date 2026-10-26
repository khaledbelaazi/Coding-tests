//! LEÇON 01 – Variables, types et fonctions
//!
//! À retenir :
//!  - Les variables sont immuables par défaut : `let x = 5;`. Pour les modifier : `let mut x = 5;`
//!  - Types : i32 / i64 (entiers), u32 / u64 (entiers positifs), f64 (décimal), bool, char, String / &str
//!  - Pas de conversion implicite : `x as f64` pour passer d'un entier à un décimal.
//!  - La dernière expression SANS point-virgule est la valeur renvoyée par la fonction.
//!
//! Consigne : remplacez chaque `todo!()` par votre code, puis lancez `cargo test lecon01`.

/// Renvoie la somme de a et b.
pub fn somme(a: i32, b: i32) -> i32 {
    a + b
}

/// Renvoie la moyenne de a et b en décimal : moyenne(3, 4) = 3.5
pub fn moyenne(a: i32, b: i32) -> f64 {
    (a + b) as f64 / 2.0
}

/// Renvoie true si n est pair (opérateur %).
pub fn est_pair(n: i32) -> bool {
    n % 2 == 0
}

/// Convertit des degrés Celsius en Fahrenheit : F = C × 9 / 5 + 32
pub fn celsius_vers_fahrenheit(celsius: f64) -> f64 {
    celsius * 9.0 / 5.0 + 32.0
}

/// Échange les deux valeurs d'un tuple : (1, 2) → (2, 1)
pub fn echanger(paire: (i32, i32)) -> (i32, i32) {
    let (a, b) = paire;
    (b, a)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_somme() {
        assert_eq!(somme(2, 3), 5);
        assert_eq!(somme(2, -3), -1);
    }

    #[test]
    fn test_moyenne() {
        assert_eq!(moyenne(3, 4), 3.5);
    }

    #[test]
    fn test_est_pair() {
        assert!(est_pair(4));
        assert!(!est_pair(7));
        assert!(est_pair(0));
    }

    #[test]
    fn test_celsius_vers_fahrenheit() {
        assert_eq!(celsius_vers_fahrenheit(0.0), 32.0);
        assert_eq!(celsius_vers_fahrenheit(100.0), 212.0);
        assert_eq!(celsius_vers_fahrenheit(-40.0), -40.0);
    }

    #[test]
    fn test_echanger() {
        assert_eq!(echanger((1, 2)), (2, 1));
    }
}
