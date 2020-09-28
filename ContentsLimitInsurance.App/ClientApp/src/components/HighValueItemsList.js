import { connect } from "react-redux";
import React, { useEffect } from "react";
import NewHighValueItemForm from "./NewHighValueItemForm";
import Category from "./Category";
import {
  loadHighValueItemsPerCategory,
  removeHighValueItemRequest,
} from "../store/thunks";
import { Card, Row, Col } from "react-bootstrap";
import "./HighValueItemsList.css";

const HighValueItemsList = ({
  categories = [],
  onRemovePressed,
  isLoading,
  startLoadingCategories,
}) => {
  useEffect(() => {
    startLoadingCategories();
  }, []);

  const loadingMessage = <div>Loading...</div>;
  const content = (
    <div>
      <Card className="category-items">
        <Card.Body>
          <NewHighValueItemForm />
          {categories.map((categoryItem) => (
            <Category
              key={categoryItem.itemCategoryId}
              items={categoryItem.items}
              category={categoryItem.category}
            />
          ))}

          <Card>
            <Card.Body>
              <Card.Title>
                <Row>
                  <Col lg={8} md={8} sm={6}>
                    TOTAL
                  </Col>
                  <Col lg={4} md={4} sm={6}>
                    $
                    {Math.round(
                      categories.reduce(function (a, b) {
                        return (
                          a +
                          b.items.reduce(function (c, d) {
                            return c + d.value;
                          }, 0)
                        );
                      }, 0) * 100
                    ) / 100}
                  </Col>
                </Row>
              </Card.Title>
            </Card.Body>
          </Card>
        </Card.Body>
      </Card>
    </div>
  );
  return isLoading ? loadingMessage : content;
};

const mapStateToProps = (state) => ({
  isLoading: state.isLoading,
  categories: state.categories,
});

const mapDispatchToProps = (dispatch) => ({
  startLoadingCategories: () => dispatch(loadHighValueItemsPerCategory()),
  onRemovePressed: (highValueItemId) =>
    dispatch(removeHighValueItemRequest(highValueItemId)),
});

export default connect(mapStateToProps, mapDispatchToProps)(HighValueItemsList);
