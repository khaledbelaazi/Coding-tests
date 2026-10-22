//! LEÇON 02 – Conditions et boucles
//!
//! À retenir :
//!  - `if` est une expression : `let x = if n > 0 { "oui" } else { "non" };`
//!  - `match` doit couvrir tous les cas, `_` = « tous les autres ».
//!  - Boucles : `for i in 1..=n` (n inclus), `for i in 1..n` (n exclu), `while condition`, `loop` + `break`.

/// Renvoie "négatif", "nul" ou "positif".
pub fn signe(n: i32) -> &'static str {
    if n < 0 {
        "négatif"
    } else if n == 0 {
        "nul"
    } else {
        "positif"
    }
}

/// FizzBuzz : "Fizz" si divisible par 3, "Buzz" par 5, "FizzBuzz" par les deux, sinon le nombre.
/// Astuce : `match (n % 3, n % 5) { (0, 0) => ..., ... }` et `n.to_string()`.
pub fn fizzbuzz(n: u32) -> String {
    match (n % 3, n % 5) {
        (0, 0) => "FizzBuzz".to_string(),
        (0, _) => "Fizz".to_string(),
        (_, 0) => "Buzz".to_string(),
        _ => n.to_string(),
    }
}

/// Renvoie n! : factorielle(0) = 1, factorielle(5) = 120.
pub fn factorielle(n: u64) -> u64 {
    let mut resultat = 1;
    for i in 2..=n {
        resultat *= i;
    }
    resultat
}

/// Renvoie true si n est premier (n ≥ 2, divisible seulement par 1 et lui-même).
pub fn est_premier(n: u32) -> bool {
    if n < 2 {
        return false;
    }
    let mut d = 2;
    while d * d <= n {
        if n % d == 0 {
            return false;
        }
        d += 1;
    }
    true
}

/// Compte les chiffres d'un nombre : 0 → 1, 12345 → 5. Utilisez une boucle et la division par 10.
pub fn compter_chiffres(n: u64) -> u32 {
    let mut n = n;
    let mut chiffres = 1;
    while n >= 10 {
        n /= 10;
        chiffres += 1;
    }
    chiffres
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_signe() {
        assert_eq!(signe(-3), "négatif");
        assert_eq!(signe(0), "nul");
        assert_eq!(signe(8), "positif");
    }

    #[test]
    fn test_fizzbuzz() {
        assert_eq!(fizzbuzz(1), "1");
        assert_eq!(fizzbuzz(9), "Fizz");
        assert_eq!(fizzbuzz(10), "Buzz");
        assert_eq!(fizzbuzz(30), "FizzBuzz");
    }

    #[test]
    fn test_factorielle() {
        assert_eq!(factorielle(0), 1);
        assert_eq!(factorielle(5), 120);
        assert_eq!(factorielle(20), 2_432_902_008_176_640_000);
    }

    #[test]
    fn test_est_premier() {
        let premiers: Vec<u32> = (0..20).filter(|&n| est_premier(n)).collect();
        assert_eq!(premiers, vec![2, 3, 5, 7, 11, 13, 17, 19]);
    }

    #[test]
    fn test_compter_chiffres() {
        assert_eq!(compter_chiffres(0), 1);
        assert_eq!(compter_chiffres(9), 1);
        assert_eq!(compter_chiffres(12345), 5);
    }
}
