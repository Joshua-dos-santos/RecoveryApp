import React, { Component } from 'react';

export class Diet extends Component {
    static displayName = Diet.name;

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
                        <th>title</th>
                    </tr>
                </thead>
                <tbody>
                    {diets.map(diet =>
                        <tr key={diet.Unique_ID}>
                            <td>{diet.Meal}</td>
                        </tr>
                    )}
                </tbody>
            </table>
            )
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : diets.renderTable(this.state.diets)

        return (
            <div>
                <h1 id="tableLabel">Diets</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
                )
    }

    async populateData() {
        const response = await fetch('api/diets');
        const data = await response.json();
        this.setState({ diets: data, loading: false });
    }


}
