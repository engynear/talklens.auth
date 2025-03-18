-- +goose Up
-- +goose StatementBegin
CREATE TABLE telegram_messages
(
    id BIGSERIAL PRIMARY KEY,
    user_id text NOT NULL,
    session_id VARCHAR(255) NOT NULL,
    telegram_user_id BIGINT NOT NULL,
    telegram_interlocutor_id BIGINT NOT NULL,
    sender_id BIGINT NOT NULL,
    message_time TIMESTAMP NOT NULL
);

CREATE INDEX idx_telegram_messages_user_session ON telegram_messages(user_id, session_id);
CREATE INDEX idx_telegram_messages_interlocutor ON telegram_messages(telegram_interlocutor_id);
CREATE INDEX idx_telegram_messages_time ON telegram_messages(message_time);
-- +goose StatementEnd

-- +goose Down
-- +goose StatementBegin
DROP TABLE IF EXISTS telegram_messages;
-- +goose StatementEnd
