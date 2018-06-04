# Football ML

A Unity + Machine learning experiment.

## How to launch

### Training

Change Brain Type to `External`.

Build for Linux with `Run In Background` enabled and the awful `Resolution Dialog` disabled.

Build in the `unity-volume` folder.

Create the docker container

1. `docker build -t foot-ml .`

2. `docker run --name FootML.01 --mount type=bind,source="$(pwd)"/unity-volume,target=/unity-volume foot-ml:latest FootML --docker-target-name=unity-volume --train --run-id=footml_01`

### Internal use of training data

Change brain to `Internal`.

## Versions

- Unity 2017.4.0f1
- [Unity ml-agents](https://github.com/Unity-Technologies/ml-agents) 0.3.1b