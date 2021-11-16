import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Diets } from './components/Diet';
import './custom.css'
import { Login } from './components/pages/Login';


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/diets' component={Diets} />
        <Route path='/fetch-data' component={FetchData} />
            <Route path='/login' component={Login} />
      </Layout>
    );
  }
}
