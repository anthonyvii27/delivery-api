CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(60) NOT NULL,
    unit_of_measurement INT NOT NULL,
    price NUMERIC(18,2) NOT NULL
);

CREATE TABLE sales (
    id SERIAL PRIMARY KEY,
    sale_date TIMESTAMP NOT NULL,
    total_amount NUMERIC(18,2) NOT NULL,
    shipping_cost NUMERIC(18,2) NOT NULL,
    zip_code VARCHAR(10) NOT NULL
);

CREATE TABLE sale_items (
    id SERIAL PRIMARY KEY,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    unit_price NUMERIC(18,2) NOT NULL,
    sale_id INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES products(id),
    FOREIGN KEY (sale_id) REFERENCES sales(id) ON DELETE CASCADE
);
