# Sample

## Setup
- Clone the repository.
- Open in UnityEditor.

## How to use:
- Open `Sample Scene`
    - Notice that there is no GameObject that exist in the scene.
    - It will be loaded during runtime.
- Just hit `Play` button.
- In Editor, currently it will instantiate the GameObject from locally.
    - To test, Go to Top Menu > `Window > AssetManagement > Addressables > Groups`
    - A `Addressable Groups` window will popup.
    - Change `Play Mode Script` dropdown to:
        - Select `Use Asset Database` to load the local prefab.
        - Select `Use Existing Build` to download from a self hosted github-pages. 


## NOTE: 
- In the github-pages: I've uploaded both cube and sphere in blue colors.
- In the build, I've committed them to be red and green colors.
- You can use it to see if it's being used from the project or downloaded from github-pages.