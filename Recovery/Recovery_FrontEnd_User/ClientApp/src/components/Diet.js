import React, { Component } from 'react';
import axios from 'axios';
import { Login } from './pages/Login';

export class Diets extends Component {
    static displayName = Diets.name;

    constructor(props) {
        super(props);

        this.state = {
            diets: [],
            
            loading: true
        };
        
    }
        componentDidMount() {
            this.populateData();        
    }

    //setDiet = event => {
    //    event.preventDefault();
    //    var token = localStorage.getItem("Key");
    //    axios({
    //        method: 'post',
    //        url: 'https://localhost:44307/api/diets/DietList',
    //        dataType: 'json',
    //        data: diets
    //    }).then(data => console.log(data));

 /*   }*/


    static renderTable(diets) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
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
                    {diets.map(diet =>
                        <tr key={diet.unique_ID}>
                            <td>{diet.meal}</td>
                            <td>{diet.calories}Kcal</td>
                            <td>{diet.protein}g</td>
                            <td>{diet.fats}g</td>
                            <td>{diet.carbohydrates}g</td>
                            <td>{diet.fibers}g</td>
                            <td><button /*onClick={(e) => this.renderTable(e)}*/>Choose Meal</button></td>
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
            url: 'https://localhost:44307/api/diets/DietList'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ diets: data.data, loading: false });
        }
        );
    }
}
