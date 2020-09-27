import * as React from "react";
import { connect } from "react-redux";

const Home = () => (
  <div>
    <h1>Contents limit insurance</h1>
    <p>List of high-value items per category</p>
    <div>
      <h1>Eletronics</h1>
      <p>$4000</p>
      <div>
        <h2>TV</h2>
        <p>$2000</p>
      </div>
      <div>
        <h2>Plays</h2>
        <p>$2000</p>
      </div>
      <div>
        <h2>TV</h2>
        <p>$2000</p>
      </div>
    </div>
  </div>
);

export default connect()(Home);
