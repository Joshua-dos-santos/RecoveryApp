import React, { Component } from 'react';
import axios from 'axios';
import './Exercises.css'
import 'bootstrap/dist/css/bootstrap.css';
import { Redirect } from 'react-router-dom';

export class Exercises extends Component {
    static displayName = Exercises.name;

    constructor(props) {
        super(props);

        this.state = {
            exercises: [],
            training: [],
            userData: [],
            jtoken: sessionStorage.getItem("token"),
            loggedIn: false,
            part_of_body: "back",
            loading: false
        };
        this.OnLoad = this.OnLoad.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.storeExercises = this.storeExercises.bind(this);
    }
    componentDidMount() {
        this.populateData();
        this.OnLoad();
    }

    OnLoad(e) {
        var self = this;
        console.log(sessionStorage.getItem("token"))
        axios({
            method: 'GET',
            url: 'http://localhost:5000/Account/GetUserByToken/GetUserByToken',
            params: {
                jtoken: sessionStorage.getItem("token")
            }
        }).then((data) => {
            console.log(data);
            self.setState({ userData: data.data }, () => { console.log(self.state.userData) });
        });
    }

    handleSubmit = (exerciseId) => {
        var self = this;
        const exerciseID = exerciseId
        const userID = self.state.userData.unique_ID
        console.log(exerciseID);
        console.log(userID)
        axios({
            method: 'POST',
            url: 'http://localhost:5000/api/exercises/UpdateExercise',
            params: { exerciseID, userID }
        }).then(function (data) {
            console.log(data.data)
        });
    }

    render() {
        if (!sessionStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
        var self = this
        console.log(self.state.training)
        let waistEx = self.state.training.filter(function (bodypart) {
            return bodypart.bodyPart == self.state.part_of_body
        });
        return (
            <div>
                <h1 id="tableLabel">Exercises</h1>
                <h2>{this.state.part_of_body}</h2>
                <p>Choose your exercise</p>
                <div>
                    <button class="btn btn-primary" id="navbutton" onClick={(e) => this.storeExercises(e)}> Store Exercises</button>
                </div>
                <div id="myBtnContainer">
                    <button class="btn btn-primary" id="navbutton" onClick={() => this.setState({ part_of_body: "waist" })}> Waist</button>
                    <button class="btn btn-primary" id="navbutton" onClick={() => this.setState({ part_of_body: "back" })}> Back</button>
                    <button class="btn btn-primary" id="navbutton" onClick={() => this.setState({ part_of_body: "chest" })}> Chest</button>
                    <button class="btn btn-primary" id="navbutton" onClick={() => this.setState({ part_of_body: "upper legs" })}> Upper Legs</button>
                    <button class="btn btn-primary" id="navbutton" onClick={() => this.setState({ part_of_body: "upper arms" })}> Upper Arms</button>
                    <button class="btn btn-primary" id="navbutton" onClick={() => this.setState({ part_of_body: "cardio" })}> Cardio</button>
                    <button class="btn btn-primary" id="navbutton" onClick={() => this.setState({ part_of_body: "shoulders" })}> Shoulders</button>
                    <button class="btn btn-primary" id="navbutton" onClick={() => this.setState({ part_of_body: "neck" })}> Neck</button>
                </div>
                <body>

                    <div className="row" >
                        {waistEx.map(exercise =>
                            <div className="col-sm-4">
                                <div className="card" id="exercises" style={{ maxHeight: "400" }}>
                                    <div className="card-body">
                                        <img className="overflow-hidden" style={{ maxHeight: "200px" }} src={exercise.gifUrl.slice(0, 4) + "s" + exercise.gifUrl.slice(4)} width="auto" height="auto"></img>
                                        <h5 className="card-title">{exercise.name}</h5>
                                        <p className="card-text"></p>
                                        <button class="btn btn-primary" onClick={(e) => {this.handleSubmit(exercise.id); alert("Submitted"+" "+ exercise.name) }}>Submit Exercise</button>
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>
                </body>
            </div>
        )
    }

    populateData = async () => {
        var self = this;
        axios({
            method: 'GET',
            url: 'https://exercisedb.p.rapidapi.com/exercises/equipment/body%20weight',
            headers: {
                'x-rapidapi-host': 'exercisedb.p.rapidapi.com',
                'x-rapidapi-key': 'bf3a34b92emsh3b4544bd33eed35p150fd1jsn4660cc5d6955'
            }
        })
            .then(function (response) {
                self.setState({ training: response.data, loading: false });
                console.log(self.state.training)
            }).catch(function (error) {
                console.error(error);
            });
    }

    storeExercises = event => {
        event.preventDefault();
        var self = this;
        for (var i = 0; i <= 324; i++) {
            var exerciseToList = {
                unique_ID: self.state.training[i].id,
                name: self.state.training[i].name,
                bodyPart: self.state.training[i].bodyPart,
                equipment: self.state.training[i].equipment,
                target: self.state.training[i].target,
                gifUrl: self.state.training[i].gifUrl
            }
            console.log(exerciseToList)
            self.state.exercises.push(exerciseToList);
        }
        const exerciseList = self.state.exercises
        console.log(exerciseToList);
        axios({
            method: 'post',
            url: 'http://localhost:5000/api/exercises/StoreExercises',
            data: exerciseList
        }).then(data => console.log(data));

    }
}

