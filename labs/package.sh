#!/bin/bash

# Define the folder names which will become the corresponding branch names
folders=("lab1-prompt-engineering" "lab2-development-use-cases" "lab3-testing")

# Initialize a new Git repository
repo_name="copilot-lab-repository"
echo "Creating Git repository: $repo_name"
mkdir "$repo_name"
cd "$repo_name" || exit

git init

# Loop through each folder and create a branch for it
for folder in "${folders[@]}"; do
    # Check if the folder exists in the parent directory
    if [ ! -d "../$folder" ]; then
        echo "Error: Folder '../$folder' does not exist. Skipping."
        continue
    fi

    branch_name="${folder}"
    echo "Adding $folder to branch $branch_name"

    # Create and switch to the new branch
    git checkout --orphan "$branch_name"

    # Remove all files from the branch to start fresh
    git rm -rf . --quiet

    # Copy the folder contents into the repository
    cp -r "../$folder"/* .

    # Add and commit the files
    git add .
    git commit -m "Add files for $folder"
done

# Switch to the first branch of the lab
branch_name="${folders[0]}"
git checkout $branch_name --quiet
