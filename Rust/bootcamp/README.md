# Bootcamp Rust

8 leçons progressives pour apprendre le Rust **en faisant passer des tests**, avec un accent sur ce qui rend Rust unique : la possession (ownership), `Option` / `Result` et les traits.

## Démarrer

Prérequis : [Rust](https://www.rust-lang.org/fr/tools/install) (installe `cargo`).

```bash
cd Rust/bootcamp
cargo test lecon01      # lance les tests de la leçon 1
cargo test              # lance tous les tests
```

Au début, tous les tests échouent avec `not yet implemented: à vous de jouer !` : c'est normal.

## Comment travailler

1. Ouvrez la leçon dans `src/` et lisez le bloc « À retenir » en haut du fichier.
2. Remplacez chaque `todo!("à vous de jouer !")` par votre code.
3. Relancez `cargo test leconXX` jusqu'à obtenir `test result: ok`.
4. Lisez attentivement les erreurs du compilateur : en Rust, elles expliquent souvent la solution.
5. Bloqué ? Le corrigé est dans `solutions/` (même nom de fichier). Essayez d'abord seul !

## Programme

| # | Leçon | Notions |
|---|---|---|
| 01 | Bases | `let`, `mut`, types, fonctions, tuples |
| 02 | Contrôle | `if` expression, `match`, `for`, `while`, `loop` |
| 03 | Ownership | possession, déplacement, emprunts `&` / `&mut`, `&str` vs `String`, durées de vie |
| 04 | Structs | `struct`, `impl`, méthodes `&self` / `&mut self`, `derive` |
| 05 | Enums | `enum` avec données, `match` exhaustif, slice patterns |
| 06 | Option et Result | absence de valeur, erreurs, opérateur `?` |
| 07 | Collections | `Vec`, `HashMap`, `HashSet`, itérateurs (`filter`, `map`, `collect`) |
| 08 | Traits | traits, méthodes par défaut, `dyn Trait`, `Display` |

> Pour aller plus loin : [le livre officiel Rust (en français)](https://jimskapt.github.io/rust-book-fr/) et le dossier `rust-book-tutorials/` de ce dépôt.
