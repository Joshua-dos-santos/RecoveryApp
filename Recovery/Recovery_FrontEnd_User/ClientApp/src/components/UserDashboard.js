import React, { Component } from 'react';
import { Link, Redirect } from "react-router-dom";
import axios from 'axios';
import { Input } from "reactstrap";
import Button from "reactstrap/lib/Button";
import Label from "reactstrap/lib/Label";
import { error } from 'jquery';

export class UserDashboard extends Component {
    static displayName = UserDashboard.name;
    constructor(props) {
        super(props);

        this.state = {
            diet: [],
            exerciseData: [],
            injuryData: [],
            userData: [],
            jtoken: sessionStorage.getItem("token"),
            loggedIn: false,
            loading: false
        };
        this.OnLoad = this.OnLoad.bind(this);
        this.getUserDiet = this.getUserDiet.bind(this);
        this.getUserExercise = this.getUserExercise.bind(this);
        this.getUserInjury = this.getUserInjury.bind(this);
    }

    componentDidMount = async () => {
        await this.OnLoad();
        await this.getUserDiet();
        await this.getUserExercise();
        await this.getUserInjury();
    }

    OnLoad = async (e) => {
        var self = this;
        console.log(sessionStorage.getItem("token"))
        await axios({
            method: 'GET',
            url: 'http://localhost:5000/Account/GetUserByToken/GetUserByToken',
            params: {
                jtoken: sessionStorage.getItem("token")
            }
        }).then((data) => {
            console.log(data);
            self.setState({ userData: data.data }, () => { console.log(self.state.userData) });
        }).catch((error) => {
            console.log(error);
        });
    }

    getUserDiet = async () => {
        var self = this;
        var id = self.state.userData.unique_ID
        console.log(id);
        await axios({
            method: 'get',
            url: 'http://localhost:5000/api/diets/GetDietByUser',
            dataType: "json",
            params: {
                userID: id
            }
        }).then((data) => {
            console.log(data.data);
            self.setState({ diet: data.data }, () => { console.log(self.state.userData) });
            console.log(self.state.diet);
        });
    }

    getUserExercise = async () => {
        var self = this;
        var id = self.state.userData.unique_ID
        console.log(id);
        await axios({
            method: 'get',
            url: 'http://localhost:5000/api/exercises/GetExerciseByUser',
            dataType: "json",
            params: {
                userID: id
            }
        }).then((data) => {
            console.log(data.data);
            self.setState({ exerciseData: data.data }, () => { console.log(self.state.exerciseData) });
            console.log(self.state.exerciseData);
        });
    }

    getUserInjury = async () => {
        var self = this;
        var id = self.state.userData.unique_ID
        console.log(id);
        await axios({
            method: 'get',
            url: 'http://localhost:5000/api/injury/GetInjuryByUser',
            dataType: "json",
            params: {
                userID: id
            }
        }).then((data) => {
            console.log(data.data);
            self.setState({ injuryData: data.data }, () => { console.log(self.state.injuryData) });
            console.log(self.state.injuryData);
        });
    }

    render() {
        if (!sessionStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
        var self = this;
        return (
            <body>
            <h1>User Dashboard</h1>
                <div class="row">
                    <div class="card mx-auto" style={{ backgroundColor: "white", maxWidth: "40%", borderRadius: "25px" }}>
                        <div class="card-body">
                            <h1>Current meal</h1>
                            <h4>{this.state.diet.meal}</h4>
                            <p> Calories: {this.state.diet.calories}Kcal<br></br>
                                Carboydrates: {this.state.diet.carbohydrates}g<br></br>
                                Protein: {this.state.diet.protein}g<br></br>
                                Fibers: {this.state.diet.fibers}g<br></br>
                                Fats: {this.state.diet.fats}g
                            </p>
                        </div>
                    </div>
                    <div class="card mx-auto" style={{
                        backgroundColor: "white", maxWidth: "40%", borderRadius:"25px" }}>
                        <div class="card-body">
                            <h1>Current Exercise</h1>
                            <h4>{this.state.exerciseData.name}</h4>
                            <img className="overflow-hidden" style={{ maxHeight: "200px" }} src={this.state.exerciseData.gifUrl} width="auto" height="auto"></img>
                            <p> Equipment: {this.state.exerciseData.equipment}Kcal<br></br>
                                Body Part: {this.state.exerciseData.bodyPart}g<br></br>
                                Target: {this.state.exerciseData.target}g<br></br>
                            </p>
                        </div>
                    </div>
                    <div class="card mx-auto" style={{ backgroundColor: "white", maxWidth: "40%", borderRadius: "25px" }}>
                        <div class="card-body">
                            <h1>Current Injury</h1>
                            <p> Body part: {this.state.injuryData.part_of_Body}<br></br>
                                Description: {this.state.injuryData.description}<br></br>
                                Pain scale: {this.state.injuryData.pain_Scale}/10<br></br>
                            </p>
                        </div>
                    </div>
                </div>
            </body>
        );
    }
}