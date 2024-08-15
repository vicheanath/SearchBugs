# Contributing to SearchsBugs

Thank you for considering contributing to SearchsBugs! Your help is essential for improving and growing this project. This document provides guidelines to help you understand how you can contribute effectively.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [How to Contribute](#how-to-contribute)
  - [Reporting Bugs](#reporting-bugs)
  - [Suggesting Features](#suggesting-features)
  - [Code Contributions](#code-contributions)
    - [Development Setup](#development-setup)
    - [Submitting Changes](#submitting-changes)
- [Style Guide](#style-guide)
- [License](#license)

## Code of Conduct

Please note that this project is governed by a [Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are expected to adhere to this code.

## Getting Started

To get started with contributing, make sure you have a basic understanding of the following technologies:

- **.NET Core** for backend development with Domain-Driven Design (DDD) principles.
- **React** for frontend development.
- **Git** for version control.

If you're unfamiliar with any of these technologies, there are plenty of resources available online to get up to speed.

## How to Contribute

### Reporting Bugs

If you encounter a bug, please open an issue on GitHub. Include as much detail as possible, such as:

- Steps to reproduce the bug.
- Expected behavior.
- Actual behavior.
- Any relevant screenshots or error messages.

### Suggesting Features

We welcome suggestions for new features! Please check the existing issues and discussions to see if your idea has already been mentioned. If not, feel free to open a new issue with a detailed description of the feature and any benefits it would bring to the project.

### Code Contributions

#### Development Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/searchsbugs.git
   cd SearchsBugs
   ```

2. **Set up the backend:**

   Ensure you have [.NET SDK](https://dotnet.microsoft.com/download) installed.

   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

3. **Set up the frontend:**

   Ensure you have [Node.js](https://nodejs.org/) and npm installed.

   ```bash
   cd SearchBugs.Ui
   npm install
   npm start
   ```

4. **Run tests:**

   - Backend tests: `dotnet test`
   - Frontend tests: `npm test`

#### Submitting Changes

1. **Fork the repository** and create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes** with clear and descriptive commit messages.

3. **Push to your fork:**
   ```bash
   git push origin feature/your-feature-name
   ```

4. **Open a pull request** on GitHub. Please include a description of your changes and reference any related issues.

## Style Guide

Adhering to a consistent style guide helps keep the codebase clean and readable.

### C# (.NET)

- Follow the [C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).
- Use meaningful names for classes, methods, and variables.
- Group related classes in the same namespace.

### JavaScript/React

- Follow the [Airbnb JavaScript Style Guide](https://github.com/airbnb/javascript).
- Use functional components and React hooks where appropriate.
- Keep components small and focused on a single responsibility.

## License

By contributing to this project, you agree that your contributions will be licensed under the project's [MIT License](LICENSE).
