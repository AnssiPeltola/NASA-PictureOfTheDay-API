# NASA Picture of the Day API Project

This project is a simple console application that interacts with NASA's Astronomy Picture of the Day (APOD) API. It allows you to download and save images from different days, either for today, yesterday, or a random day. You can choose between HD and Standard quality images.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)

## Features

- Download and save today's picture
- Download and save yesterday's picture
- Download and save a random day's picture
- Choose between HD and Standard quality images

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

### Clone the Repository

```bash
git clone https://github.com/AnssiPeltola/NASA-PictureOfTheDay-API.git
```

### Build and Run

Navigate to the project directory and run the following commands:

```bash
cd NASA-PictureOfTheDay-API
dotnet build
dotnet run
```

## Usage

Upon running the application, you'll be presented with the following options:

- `1`: Today's picture
- `2`: Yesterday's picture
- `3`: Random day's picture
- `4`: Exit

Choose an option by typing the corresponding number and press Enter. If you choose to download an image, you'll be prompted to select the image quality: HD or Standard.
