//! LEÇON 07 – Collections et itérateurs : Vec, HashMap, HashSet
//!
//! À retenir :
//!  - `Vec<T>` : liste extensible. `vec![1, 2, 3]`, `v.push(4)`, `v.len()`
//!  - `HashMap<K, V>` : dictionnaire. `*map.entry(cle).or_insert(0) += 1;` pour compter.
//!  - Itérateurs (comme LINQ en C#) : `iter()`, `filter`, `map`, `sum`, `max`, `collect`
//!    exemple : `v.iter().filter(|&&x| x > 0).count()`

use std::collections::{HashMap, HashSet};

/// Plus grand élément, ou None si la liste est vide.
pub fn maximum(valeurs: &[i32]) -> Option<i32> {
    todo!("à vous de jouer !")
}

/// Carrés des nombres pairs, dans l'ordre : [1, 2, 3, 4] → [4, 16]
pub fn carres_des_pairs(valeurs: &[i32]) -> Vec<i32> {
    todo!("à vous de jouer !")
}

/// Compte les mots d'un texte (séparés par des espaces), en minuscules.
pub fn compter_mots(texte: &str) -> HashMap<String, usize> {
    todo!("à vous de jouer !")
}

/// Supprime les doublons en gardant l'ordre de première apparition.
pub fn sans_doublons(valeurs: &[i32]) -> Vec<i32> {
    todo!("à vous de jouer !")
}

/// Moyenne des notes de chaque élève : { "Léa": [12, 16] } → { "Léa": 14.0 }
pub fn moyennes(notes: &HashMap<String, Vec<f64>>) -> HashMap<String, f64> {
    todo!("à vous de jouer !")
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_maximum() {
        assert_eq!(maximum(&[3, 9, 2]), Some(9));
        assert_eq!(maximum(&[]), None);
    }

    #[test]
    fn test_carres_des_pairs() {
        assert_eq!(carres_des_pairs(&[1, 2, 3, 4]), vec![4, 16]);
    }

    #[test]
    fn test_compter_mots() {
        let c = compter_mots("Le chat et le chien");
        assert_eq!(c["le"], 2);
        assert_eq!(c["chat"], 1);
        assert_eq!(c.len(), 4);
    }

    #[test]
    fn test_sans_doublons() {
        assert_eq!(sans_doublons(&[3, 1, 3, 2, 1]), vec![3, 1, 2]);
    }

    #[test]
    fn test_moyennes() {
        let mut notes = HashMap::new();
        notes.insert("Léa".to_string(), vec![12.0, 16.0]);
        notes.insert("Karim".to_string(), vec![10.0]);
        let m = moyennes(&notes);
        assert_eq!(m["Léa"], 14.0);
        assert_eq!(m["Karim"], 10.0);
    }
}
