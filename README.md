# Football ML

A Unity + Machine learning experiment.

## How to launch

### Training

Change Brain Type to `External`.

Build for Linux x86_64 with `Run In Background` enabled and the awful `Resolution Dialog` disabled.

Build in the `unity-volume` folder.

Create the docker container

1. `docker build -t foot-ml .`

2. `docker run --name FootML.01 --mount type=bind,source="$(pwd)"/unity-volume,target=/unity-volume foot-ml:latest FootML --docker-target-name=unity-volume --train --run-id=footml_01`

### Internal use of training data

If not done yet, download and import the [Unity TensorFlow Plugin](https://s3.amazonaws.com/unity-ml-agents/0.3/TFSharpPlugin.unitypackage)
(It's too large for git)

Change brain to `Internal`.

Set scripting symbol `ENABLE_TENSORFLOW`. Make sure you're using .NET 4.6.

## Versions

- Unity 2017.4.0f1
- [Unity ml-agents](https://github.com/Unity-Technologies/ml-agents) 0.3.1b