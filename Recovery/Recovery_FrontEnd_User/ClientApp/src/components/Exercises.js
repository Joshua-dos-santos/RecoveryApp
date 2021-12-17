import React, { Component } from 'react';
import axios from 'axios';
import './Exercises.css'
import 'bootstrap/dist/css/bootstrap.css';

export class Exercises extends Component {
    static displayName = Exercises.name;

    constructor(props) {
        super(props);

        this.state = {
            exercises: [],
            part_of_body: "back",
            loading: false
        };
    }
    componentDidMount() {
        this.populateData();
    }

     static render(exercises) {
        return (
            <body>
                
                <div className="row">
                    {exercises.map(exercise =>
                        <div className="col-sm-6">
                            <div className="card" style={{ maxWidth: "300px" }}>
                                <div className="card-body">
                                    <img className="overflow-hidden" style={{ maxHeight: "200px" }} src={exercise.gifUrl.slice(0, 4) + "s" + exercise.gifUrl.slice(4)} width="auto" height="auto"></img>
                                    <h5 className="card-title">{exercise.name }</h5>
                                    <p className="card-text"></p>
                                    <a href="#" class="btn btn-primary">Go somewhere</a>
                                </div>
                            </div>
                        </div>
                    )}
                </div>
            </body>


        )
    }

    render() {
        var self = this
        console.log(self.state.exercises)
        let waistEx = self.state.exercises.filter(function (bodypart) {
            return bodypart.bodyPart == self.state.part_of_body
        });
        let contents = self.state.loading
            ? <p><em>Loading...</em></p>
            : Exercises.render(waistEx)
        return (
            <div>
                <h1 id="tableLabel">Exercises</h1>
                <h2>{this.state.part_of_body}</h2>
                <p>Choose your exercise</p>
                <div id="myBtnContainer">
                    <button class="btn btn-primary" onClick={() => this.setState({ part_of_body: "waist" })}> Waist</button>
                    <button class="btn btn-primary" onClick={() => this.setState({ part_of_body: "back" })}> Back</button>
                    <button class="btn btn-primary" onClick={() => this.setState({ part_of_body: "chest" })}> Chest</button>
                    <button class="btn btn-primary" onClick={() => this.setState({ part_of_body: "upper legs" })}> Upper Legs</button>
                    <button class="btn btn-primary" onClick={() => this.setState({ part_of_body: "upper arms" })}> Upper Arms</button>
                    <button class="btn btn-primary" onClick={() => this.setState({ part_of_body: "cardio" })}> Cardio</button>
                </div>
                {contents}
            </div>
        )
    }

    populateData = async () => {
        var self = this;
        localStorage.clear();
        axios({
            method: 'GET',
            url: 'https://exercisedb.p.rapidapi.com/exercises/equipment/body%20weight',
            headers: {
                'x-rapidapi-host': 'exercisedb.p.rapidapi.com',
                'x-rapidapi-key': 'bf3a34b92emsh3b4544bd33eed35p150fd1jsn4660cc5d6955'
            }
        })
            .then(function (response) {
                self.setState({ exercises: response.data, loading: false });
            }).catch(function (error) {
                console.error(error);
            });
    }
}

