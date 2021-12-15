import React, { Component } from 'react';
import axios from 'axios';
import './Diet.css'
import { data } from 'jquery';
import ReactSession from 'react-client-session/dist/ReactSession';
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
            jtoken: localStorage.getItem("token"),
            loggedIn: false
        };

        this.OnLoad = this.OnLoad.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.storeMeals = this.storeMeals.bind(this);
    }
    componentDidMount() {
        this.populateData();
        this.OnLoad();
    }

    OnLoad = (e) => {
        var self = this;
        console.log(localStorage.getItem("token"))
        axios({
            method: 'GET',
            url: 'http://localhost:5000/Account/GetUserByToken/GetUserByToken',
            params: {
                jtoken: localStorage.getItem("token")
            }
        }).then((data) => {
            console.log(data);
            self.setState({ userData: data.data }, () => { console.log(self.state.userData) });
        });
    }

    handleSubmit = (event) => {
        event.preventDefault();
        var self = this;
        const mealID = 715594
        const userID = self.state.userData.unique_ID
        console.log(mealID);
        console.log(userID)
        axios({
            method: 'post',
            url: 'http://localhost:5000/api/diets/UpdateMeal',
            dataType: "json",
            data: { mealID, userID }
        }).then(data => console.log(data));
    }



    static renderTable(diets) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel" id="meals">
                <thead>
                    <tr>
                        <th>Meal</th>
                        <th>Calories</th>
                        <th>Protein</th>
                        <th>Fats</th>
                        <th>Carbohydrates</th>
                        <th>Fibers</th>
                    </tr>
                </thead>
                <tbody>
                    {diets.results.map(diet =>
                        <tr key={diet.id}>
                            <td>{diet.title}</td>
                            <td>{diet.nutrition.nutrients[0].amount}Kcal</td>
                            <td>{diet.nutrition.nutrients[1].amount}g</td>
                            <td>{diet.nutrition.nutrients[2].amount}g</td>
                            <td>{diet.nutrition.nutrients[3].amount}g</td>
                            <td>{diet.nutrition.nutrients[4].amount}g</td>
                            
                        </tr>
                    )}
                </tbody>
            </table>
        )
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Diets.renderTable(this.state.diets)

        if (!localStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
        return (
            <div>
                <h1 id="tableLabel">Diets</h1>
                <p>Choose your diet meal</p><button class="btn btn-primary" onClick={(e) => this.storeMeals(e)}>Store Meal</button>
                <button class="btn btn-primary" onClick={(e) => this.handleSubmit(e)}>Choose Meal</button>

                {contents}
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
            self.setState({ diets: data.data, loading: false });
        }
        );
    }

    storeMeals = event => {
        event.preventDefault();
        var self = this;
        for (var i = 0; i <= 9; i++) {
            var mealToList = {
                unique_ID: self.state.diets.results[i].id,
                meal: self.state.diets.results[i].title,
                protein: self.state.diets.results[i].nutrition.nutrients[1].amount,
                fats: self.state.diets.results[i].nutrition.nutrients[2].amount,
                carbohydrates: self.state.diets.results[i].nutrition.nutrients[3].amount,
                calories: self.state.diets.results[i].nutrition.nutrients[0].amount,
                fibers: self.state.diets.results[i].nutrition.nutrients[4].amount
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