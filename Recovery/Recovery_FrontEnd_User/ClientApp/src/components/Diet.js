import React, { Component } from 'react';
import axios from 'axios';
import './Diet.css'
import { data } from 'jquery';

export class Diets extends Component {
    static displayName = Diets.name;

    constructor(props) {
        super(props);

        this.state = {
            diets: [],
            meal: '',
            calories: '',
            protein: '',
            fats: '',
            carbohydrates: '',
            fibers: '',
            loading: true
        };
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    componentDidMount() {
        this.populateData();
    }

    handleSubmit = event => {
        event.preventDefault();
        const mealID = this.state.diets.results.id
        console.log(mealID);
        //axios({
        //    method: 'post',
        //    url: 'https://localhost:5000/api/diets/UpdateMeal',
        //    dataType: "json",
        //    data: mealID
        //}).then(data => console.log(data));
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
                            <td><button class="btn btn-primary" onClick={(e) => this.handleSubmit(e)}>Choose Meal</button></td>
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
            method: 'get',
            url: 'https://api.spoonacular.com/recipes/complexSearch?apiKey=47bc84e2dca9462ea4524639a51ea75d&minProtein=10&minCalories=50&minFat=1&minFiber=0&minCarbs=10&number=20'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ diets: data.data, loading: false });
        }
        );
    }
}