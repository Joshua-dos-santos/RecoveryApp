﻿import React, { Component } from 'react';
import axios from 'axios';
import './Diet.css'
import { Redirect } from 'react-router-dom';


export class Diets extends Component {
    static displayName = Diets.name;

    constructor(props) {
        super(props);

        this.state = {
            diets: [],
            meals: [],
            mealId: '',
            calories: '',
            protein: '',
            fats: '',
            carbohydrates: '',
            fibers: '',
            loading: true,
            userData: [],
            jtoken: sessionStorage.getItem("token"),
            loggedIn: false
        };

        this.OnLoad = this.OnLoad.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.storeMeals = this.storeMeals.bind(this);
    }
    componentDidMount() {
        this.OnLoad();
        this.populateData();
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


    handleSubmit = (mealId) => {
        var self = this;
        const mealID = mealId
        const userID = self.state.userData.unique_ID
        console.log(mealID);
        console.log(userID)
        axios({
            method: 'POST',
            url: 'http://localhost:5000/api/diets/UpdateMeal',
            params: { mealID, userID }
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
        return (
            <div>
                <h1 id="tableLabel">Diets</h1>
                <p>Choose your diet meal</p><button class="btn btn-primary" onClick={(e) => this.storeMeals(e)}>Store Meal</button> 
                <table className='table table-striped' aria-labelledby="tabelLabel" id="meals">
                    <thead>
                        <tr>
                            <th>Meal</th>
                            <th>Calories</th>
                            <th>Protein</th>
                            <th>Fats</th>
                            <th>Carbohydrates</th>
                            <th>Fibers</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.diets.map(diet =>
                            <tr key={diet.id}>
                                <td>{diet.title}</td>
                                <td>{diet.nutrition.nutrients[0].amount}Kcal</td>
                                <td>{diet.nutrition.nutrients[1].amount}g</td>
                                <td>{diet.nutrition.nutrients[2].amount}g</td>
                                <td>{diet.nutrition.nutrients[3].amount}g</td>
                                <td>{diet.nutrition.nutrients[4].amount}g</td>
                                <td><button class="btn btn-primary" onClick={(e) => { this.handleSubmit(diet.id); alert("Submitted"+" "+ diet.title) }}>Submit Meal</button></td>

                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        )
    }

    populateData = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'https://api.spoonacular.com/recipes/complexSearch?apiKey=47bc84e2dca9462ea4524639a51ea75d&minProtein=10&minCalories=50&minFat=1&minFiber=0&minCarbs=10&number=10'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ diets: data.data.results, loading: false });
        }
        );
    }

    storeMeals = event => {
        event.preventDefault();
        var self = this;
        for (var i = 0; i <= 9; i++) {
            var mealToList = {
                unique_ID: self.state.diets[i].id,
                meal: self.state.diets[i].title,
                protein: self.state.diets[i].nutrition.nutrients[1].amount,
                fats: self.state.diets[i].nutrition.nutrients[2].amount,
                carbohydrates: self.state.diets[i].nutrition.nutrients[3].amount,
                calories: self.state.diets[i].nutrition.nutrients[0].amount,
                fibers: self.state.diets[i].nutrition.nutrients[4].amount
            }
            console.log(mealToList)
            self.state.meals.push(mealToList);
        }
        const mealList = self.state.meals
        console.log(mealList);
        axios({
            method: 'post',
            url: 'http://localhost:5000/api/diets/StoreMeals',
            data: mealList
        }).then(data => console.log(data));

    }
}