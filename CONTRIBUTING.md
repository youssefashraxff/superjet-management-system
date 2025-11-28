# Contributing Guide

Hello Ali w Toqa e2ro el kalam kowys

---

## 1. First-Time Setup

1. Clone the repo:
   ```bash
   git clone <repo-url>
   cd superjet-management
   ```
2. Switch to the dev branch:
   ```bash
   git checkout dev
   git pull origin dev
   ```
3. Open the project in VS Code:
   ```bash
   code .
   ```

---

## 2. Branching Rules

We use 3 main types of branches:

- **main** → stable, final code (no direct pushes)
- **dev** → where all features get merged
- **feature/<name>-desc** → your working branch

### Creating a feature branch (always from dev):

```bash
git checkout dev
git pull origin dev
git checkout -b feature/<yourname>-short-desc
```

Examples:

- `feature/ali-routes-crud`
- `feature/toqa-ui-layout`
- `feature/youssef-ticket-controller`

---

## 3. Working on Your Branch

1. Add your changes.
2. Stage and commit:
   ```bash
   git add .
   git commit -m "feat: clear short description"
   ```
3. Push your branch:
   ```bash
   git push -u origin feature/<yourname>-short-desc
   ```

---

## 4. Opening a Pull Request (PR)

On GitHub:

1. Open a PR from your feature branch → **dev**
2. Title format:
   - `feat: add routes CRUD`
   - `fix: ticket validation issue`
3. Description: explain what you did in 1–3 lines.
4. Request a reviewer.
5. After approval, merge into **dev**.
6. Delete your feature branch (GitHub will show a button).

---

## 5. Commit Message Rules (Simple)

Use short, clear prefixes:

- `feat:` for new features
- `fix:` for bug fixes
- `chore:` for setup/cleanup
- `docs:` for documentation
- `style:` for UI changes

Example:

```
git commit -m "feat: add route model and controller"
```

---

## 6. How to Run the Project

From repo root:

```bash
cd src/Superjet.Web
dotnet restore
dotnet build
dotnet run
```

App will start on `https://localhost:<port>`.

---

## 7. PR Checklist (Before Merging)

- [ ] Branch created from `dev`
- [ ] Project builds with `dotnet build`
- [ ] No debugging code (Console.WriteLine, etc.)
- [ ] Clear commit messages
- [ ] PR has a short description

---
