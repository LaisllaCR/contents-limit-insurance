import React, { useState } from "react";
import { connect } from "react-redux";
import { addHighValueItemRequest } from "./../store/thunks";
import "./NewHighValueItemForm.css";

class NewHighValueItemForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      name: "",
      value: 0,
      itemCategoryId: 1,
    };

    this.handleInputChange = this.handleInputChange.bind(this);
  }

  handleInputChange(event) {
    const target = event.target;
    const value = target.type === "checkbox" ? target.checked : target.value;
    const name = target.name;

    this.setState({
      [name]: value,
    });
  }

  render() {
    //const NewHighValueItemForm = ({ items, onCreatePressed }) => {
    //const [inputValue, setInputValue] = useState("");

    return (
      <div className="new-todo-form">
        <input
          name="name"
          className="new-todo-input"
          type="text"
          maxLength="50"
          placeholder="Name"
          value={this.state.name}
          onChange={this.handleInputChange}
        />
        <input
          name="value"
          className="new-todo-input"
          type="number"
          placeholder="Value"
          maxLength="6"
          title="Value"
          value={this.state.value}
          onChange={this.handleInputChange}
        />

        <select
          name="itemCategoryId"
          title="Category"
          value={this.state.itemCategoryId}
          onChange={this.handleInputChange}
          className="new-todo-input"
        >
          <option value="1">Eletronics</option>
          <option value="2">Clothing</option>
          <option value="3">Kitchen</option>
        </select>
        <br></br>
        <button
          onClick={() => {
            const isDuplicateText = this.props.items.some(
              (item) => item.name === this.state.name
            );
            if (!isDuplicateText) {
              this.props.onCreatePressed(
                this.state.name,
                this.state.value,
                this.state.itemCategoryId
              );
              this.setState({
                name: "",
                value: 0,
                itemCategoryId: 1,
              });
            }
          }}
          className="new-todo-button"
        >
          Create Item
        </button>
      </div>
    );
  }
}
const mapStateToProps = (state) => ({
  items: state.items,
});

const mapDispatchToProps = (dispatch) => ({
  onCreatePressed: (name, value, itemCategoryId) =>
    dispatch(addHighValueItemRequest(name, value, itemCategoryId)),
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(NewHighValueItemForm);
