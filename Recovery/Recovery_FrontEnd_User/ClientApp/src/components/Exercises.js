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
                            <div className="card">
                                <div className="card-body">
                                    <iframe className="overflow-hidden" src={exercise.gifUrl.slice(0, 4) + "s" + exercise.gifUrl.slice(4)} width="500" height="360"></iframe>
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
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Exercises.render(this.state.exercises)

        return (
            <div>
                <h1 id="tableLabel">Diets</h1>
                <p>Choose your diet meal</p>
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

