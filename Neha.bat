git config --global user.name "SNehaHafiz”
git config --global user.email "neha@digidisruptors.com" 
git init
git add .
git commit -m "Purchase_29_03_2023"
git remote add origin  https://github.com/kumaranand30/NavkarCFSMain-2023.git
git fetch --all
git checkout -b "dev/NCFS_29_03_2023"
git push origin dev/NCFS_29_03_2023

