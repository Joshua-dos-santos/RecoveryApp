import React, { Component } from 'react';
import axios from 'axios';

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
                            <td></td>
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
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
                )
    }

    populateData = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'https://localhost:44307/api/diets'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ diets: data.data, loading: false });
        }
        );
        //const response = await fetch('https://localhost:44307/api/diets');
        //const data = await response.json();
        //this.setState({ diets: data, loading: false });
    }


}
